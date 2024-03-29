﻿using CromWood.Business.Constants;
using CromWood.Business.Helper;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using AutoMapper;

namespace CromWood.Business.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private List<RolePermission> rolePermissions = new();

        public AuthService(IConfiguration config, IUserRepository userRepo, IHttpContextAccessor httpContextAccessor, IMemoryCache cache, IMapper mapper)
        {
            _userRepo = userRepo;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _mapper = mapper;
        }

        public UserClaimModel GetCurrentUser(ClaimsPrincipal identity)
        {
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserClaimModel
                {
                    UserId = Guid.Parse(userClaims.First(o => o.Type == ClaimTypes.NameIdentifier).Value),
                    Email = userClaims.First(o => o.Type == ClaimTypes.Email).Value,
                    RoleId = Guid.Parse(userClaims.First(o => o.Type == ClaimTypes.Role).Value),
                };
            }
            return new UserClaimModel();
        }

        public async Task<AppResponse<string>> Login(LoginModel login)
        {
            try
            {
                var user = await Authenticate(login.Email, login.Password);
                if (user != null)
                {
                    var result = await GenerateLogin(user, login.RememberMe);
                    // Update user LastActiveDate to null 
                    user.LastActive = null;
                    await _userRepo.SetUserActive(user);
                    return ResponseCreater<string>.CreateSuccessResponse(result, "User login found.");
                }
                else
                {
                    return ResponseCreater<string>.CreateNotFoundResponse(null, "User not found.");
                }
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<User>> GetLoggedInUser()
        {
            try
            {
                var UserId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
                var user = await _userRepo.GetUser(UserId);
                return ResponseCreater<User>.CreateSuccessResponse(user, "User loaded successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<User>.CreateErrorResponse(null, ex.ToString());
            }
        }


        public async Task<AppResponse<string>> Logout()
        {
            try
            {
                if (_httpContextAccessor.HttpContext.User != null)
                {
                    var userId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
                    await _httpContextAccessor.HttpContext.SignOutAsync();
                    await _userRepo.SetUserInactive(userId);
                    return ResponseCreater<string>.CreateSuccessResponse("Success", "User logged out successfully.");
                }
                else
                {
                    return ResponseCreater<string>.CreateNotFoundResponse(null, "User not logged in.");
                }
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        #region Private Methods
        private async Task<string> GenerateLogin(User user, bool rememberMe = false)
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Email,user.Email),
                new System.Security.Claims.Claim(ClaimTypes.Role,user.RoleId.ToString()),
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authPropertues = new AuthenticationProperties()
            {
                IsPersistent = rememberMe,
            };
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authPropertues);

            return "Success";
        }

        private async Task<User> Authenticate(string userName, string password)
        {
            var user = await _userRepo.GetUser(userName, PasswordHasher.Password2hash(password));
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> CheckPermission(string permissionKey, string permissionMatch)
        {
            if (_httpContextAccessor.HttpContext.User != null)
            {
                Guid RoleId = IdentityExtension.GetRoleId(_httpContextAccessor.HttpContext.User);
                _cache.TryGetValue("role_permission", out rolePermissions);
                if (rolePermissions == null || rolePermissions.Count == 0)
                {
                    rolePermissions = await _userRepo.GetAllRolesAndPermission();
                    _cache.Set("role_permission", rolePermissions);
                }
                // Now check the role is fine or not. 
                var roleWithPermission = rolePermissions.Where(x => x.RoleId == RoleId);
                bool havePermission = false;

                switch (permissionMatch)
                {
                    case PermissionConstant.ViewAll:
                        havePermission = roleWithPermission.Any(x => x.Permission.PermissionKey == permissionKey && x.CanViewAll);
                        break;

                    case PermissionConstant.CanRead:
                        havePermission = roleWithPermission.Any(x => x.Permission.PermissionKey == permissionKey && x.CanRead);
                        break;

                    case PermissionConstant.CanWrite:
                        havePermission = roleWithPermission.Any(x => x.Permission.PermissionKey == permissionKey && x.CanWrite);
                        break;

                    case PermissionConstant.CanDelete:
                        havePermission = roleWithPermission.Any(x => x.Permission.PermissionKey == permissionKey && x.CanDelete);
                        break;

                    default:
                        break;
                }
                return havePermission;
            }
            return false;
        }
        #endregion

        #region Forgot password
        public async Task<bool> SendOTP(ForgotPasswordModel model)
        {
            var user = await _userRepo.GetUser(model.Email);
            if (user == null) return false;
            var otp = RandomAlphaNumbericGenerator.Random(6);
            // Save OTP to user table
            var result = await _userRepo.SetOTPForUser(model.Email, otp);
            // Sending Email to this
            EmailSender.SendOTPEmail(result, otp);
            return result != null;
        }

        public async Task<bool> VerifyOTP(ForgotPasswordModel model)
        {
            var user = await _userRepo.GetUser(model.Email);
            if (user == null) return false;
            if(model.OTP == user.OTP && user.OTPExpirationDate > DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public async Task<AppResponse<string>> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                var user = await _userRepo.GetUser(model.Email);
                if (user == null) return ResponseCreater<string>.CreateNotFoundResponse("User not found");
                if (model.OTP == user.OTP && user.Email == model.Email && user.Password != null)
                {
                    user.Password = PasswordHasher.Password2hash(model.Password);
                    var result = await _userRepo.UpdateUserPassword(user);
                    if (result == 1)
                    {
                        return ResponseCreater<string>.CreateSuccessResponse("success", "Password Reset successfull");
                    }
                    else
                    {
                        return ResponseCreater<string>.CreateErrorResponse(null, "Error occured when updating password.");
                    }
                }
                else
                {
                    return ResponseCreater<string>.CreateErrorResponse(null, "Error occured when updating password.");
                }
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        #endregion
    }
}
