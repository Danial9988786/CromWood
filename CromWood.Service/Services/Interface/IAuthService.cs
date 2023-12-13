using CromWood.Business.Helper;
using CromWood.Business.ViewModels;
using System.Security.Claims;

namespace CromWood.Business.Services.Interface
{
    public interface IAuthService
    {
        public UserClaimModel GetCurrentUser(ClaimsIdentity identity);
        public Task<Response<string>> Login(LoginViewModel login);
        public Task<Response<string>> Logout();
    }
}
