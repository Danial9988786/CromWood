using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;
using System.Security.Claims;

namespace CromWood.Business.Services.Interface
{
    public interface IAuthService
    {
        public UserClaimModel GetCurrentUser(ClaimsPrincipal identity);
        public Task<AppResponse<string>> Login(LoginModel login);
        public Task<AppResponse<string>> ResetPassword(ResetPasswordModel model);
        public Task<AppResponse<string>> Logout();
        public Task<bool> CheckPermission(string permissionKey, string permissionMatch);
        public Task<bool> SendOTP(ForgotPasswordModel model);
        public Task<bool> VerifyOTP(ForgotPasswordModel model);
        public Task<AppResponse<User>> GetLoggedInUser();
    }
}
