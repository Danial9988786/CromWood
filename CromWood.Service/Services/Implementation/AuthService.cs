using CromWood.Business.Helper;
using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace CromWood.Business.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration config, IUserRepository userRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _httpContextAccessor = httpContextAccessor;
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
                    var result = await GenerateLogin(user);
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

        public async Task<AppResponse<string>> Logout()
        {
            try
            {
                if (_httpContextAccessor.HttpContext.User != null)
                {
                    await _httpContextAccessor.HttpContext.SignOutAsync();
                    var userId = Guid.Parse(IdentityExtension.GetId(_httpContextAccessor.HttpContext.User.Identity));
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
        private async Task<string> GenerateLogin(User user)
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
        #endregion
    }
}
