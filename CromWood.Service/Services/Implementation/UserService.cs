using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace CromWood.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserService(IUserRepository userRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor, IFileUploader uploader)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _fileUploader = uploader;
        }

        public async Task<AppResponse<IEnumerable<UserViewModel>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepo.GetAllUsers();
                var mapppedUsers = _mapper.Map<IEnumerable<UserViewModel>>(users);
                return ResponseCreater<IEnumerable<UserViewModel>>.CreateSuccessResponse(mapppedUsers, "Users loaded successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<UserViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<UserViewModel>> GetUserById(Guid Id)
        {
            try
            {
                var users = await _userRepo.GetUser(Id);
                var mapppedUsers = _mapper.Map<UserViewModel>(users);
                return ResponseCreater<UserViewModel>.CreateSuccessResponse(mapppedUsers, "User loaded successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<UserViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> InviteUser(UserModel user)
        {
            try
            {
                
                var mappedUser = _mapper.Map<User>(user);
                mappedUser.Password = PasswordHasher.Password2hash(mappedUser.Password);
                var result = await _userRepo.InviteUser(mappedUser);
                if (result == 1)
                {
                    EmailSender.SendInvitationEmail(user.Email, user.Password);
                }
                return ResponseCreater<string>.CreateSuccessResponse(null, "User invited successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<string>> ChangeUserRole(Guid UserId, Guid RoleId)
        {
            try
            {
                await _userRepo.ChangeUserRole(UserId, RoleId);
                return ResponseCreater<string>.CreateSuccessResponse(null, "User role changed successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<string>> BlockUserById(Guid Id)
        {
            try
            {
                var result = await _userRepo.BlockUserById(Id);
                return ResponseCreater<string>.CreateSuccessResponse(result, "User blocked successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<string>> DeleteUserById(Guid Id)
        {
            try
            {
                var result = await _userRepo.DeleteUserById(Id);
                return ResponseCreater<string>.CreateSuccessResponse(result, "User deleted Successfully");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> UpdateFirstName(string FirstName)
        {
            try
            {
                var UserId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
                var user = await _userRepo.GetUser(UserId);
                if(user == null)
                {
                    return ResponseCreater<string>.CreateNotFoundResponse("User not found.");
                }
                user.FirstName = FirstName;
                var result = await _userRepo.UpdateUser(user);
                return ResponseCreater<string>.CreateSuccessResponse(result, "First name updated successfully.");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> UpdateLastName(string LastName)
        {
            try
            {
                var UserId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
                var user = await _userRepo.GetUser(UserId);
                if (user == null)
                {
                    return ResponseCreater<string>.CreateNotFoundResponse("User not found.");
                }
                user.LastName = LastName;
                var result = await _userRepo.UpdateUser(user);
                return ResponseCreater<string>.CreateSuccessResponse(result, "Last name updated successfully.");

            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> UpdatePhone(string Phone)
        {
            try
            {
                var UserId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
                var user = await _userRepo.GetUser(UserId);
                if (user == null)
                {
                    return ResponseCreater<string>.CreateNotFoundResponse("User not found.");
                }
                user.Phone = Phone;
                var result = await _userRepo.UpdateUser(user);
                return ResponseCreater<string>.CreateSuccessResponse(result, "Phone updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> UpdateAvatar(IFormFile file)
        {
            try
            {
                var UserId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
                var user = await _userRepo.GetUser(UserId);
                if (user == null)
                {
                    return ResponseCreater<string>.CreateNotFoundResponse("User not found.");
                }
                var url = "";
                if (file != null)
                {
                    url = await _fileUploader.Upload(file, "avatar");
                }
                user.AvatarUrl = url;
                var result = await _userRepo.UpdateUser(user);
                return ResponseCreater<string>.CreateSuccessResponse(url, "Avatar updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> UpdatePassword(ResetPasswordModel model)
        {
            try
            {
                var UserId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
                var user = await _userRepo.GetUser(UserId);
                if (user == null)
                {
                    return ResponseCreater<string>.CreateNotFoundResponse("User not found.");
                }
                if (user.Password != PasswordHasher.Password2hash(model.OldPassword))
                {
                    return ResponseCreater<string>.CreateErrorResponse("Old password doesn't matched.");
                }
                user.Password = PasswordHasher.Password2hash(model.Password);
                var result = await _userRepo.UpdateUser(user);
                return ResponseCreater<string>.CreateSuccessResponse(result, "Password updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
