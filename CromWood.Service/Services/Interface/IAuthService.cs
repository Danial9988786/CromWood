using CromWood.Business.Helper;
using CromWood.Business.ViewModels;
using System.Security.Claims;

namespace CromWood.Business.Services.Interface
{
    public interface IAuthService
    {
        public UserClaimModel GetCurrentUser(ClaimsPrincipal identity);
        public Task<AppResponse<string>> Login(LoginModel login);
        public Task<AppResponse<string>> Logout();
    }
}
