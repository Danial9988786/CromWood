using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IUserRepository
    {
        #region For Logins
        public Task<User> GetUser(string email, string password);
        public Task<User> GetUser(Guid Id);
        public bool CheckEmail(string email);
        public Task<List<RolePermission>> GetAllRolesAndPermission();
        #endregion

        #region Menu items for Admin
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<int> InviteUser(User user);
        public Task<int> ChangeUserRole(Guid UserId, Guid RoleId);
        public Task<string> BlockUserById(Guid Id);
        public Task<int> SetUserActive(User user);
        public Task<int> SetUserInactive(Guid Id);
        public Task<string> DeleteUserById(Guid Id);

        #endregion
    }
}
