
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Services.Interface
{
    public interface IUserService
    {
        #region Admin accessible features
        public Task<AppResponse<IEnumerable<UserViewModel>>> GetAllUsersAsync();
        public Task<AppResponse<UserViewModel>> GetUserById(Guid Id);
        public Task<AppResponse<string>> InviteUser(UserModel user);
        public Task<AppResponse<string>> ChangeUserRole(Guid UserId, Guid RoleId);
        public Task<AppResponse<string>> BlockUserById(Guid Id);
        public Task<AppResponse<string>> DeleteUserById(Guid Id);

        #endregion

        #region Profile settings update
        public Task<AppResponse<string>>  UpdateFirstName(string FirstName);
        public Task<AppResponse<string>>  UpdateLastName(string LastName);
        public Task<AppResponse<string>>  UpdatePhone(string Phone);
        public Task<AppResponse<string>>  UpdateAvatar(IFormFile file);
        public Task<AppResponse<string>>  UpdatePassword(ResetPasswordModel model);
        #endregion
    }
}
