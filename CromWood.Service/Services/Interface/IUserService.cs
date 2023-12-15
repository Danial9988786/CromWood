
using CromWood.Business.Helper;
using CromWood.Business.Models;

namespace CromWood.Business.Services.Interface
{
    public interface IUserService
    {
        #region Admin accessible features
        public Task<AppResponse<IEnumerable<UserViewModel>>> GetAllUsersAsync();
        public Task<AppResponse<string>> InviteUser(UserModel user);
        public Task<AppResponse<string>> ChangeUserRole(Guid UserId, Guid RoleId);
        public Task<AppResponse<string>> BlockUserById(Guid Id);
        public Task<AppResponse<string>> DeleteUserById(Guid Id);

        #endregion
    }
}
