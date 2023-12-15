using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IUserRepository
    {
        #region For Logins
        public Task<User> GetUser(string email, string password);
        public bool CheckEmail(string email);
        #endregion

        #region Menu items for Admin
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<int> InviteUser(User user);
        public Task<int> ChangeUserRole(Guid UserId, Guid RoleId);
        public Task<int> BlockUserById(Guid Id);
        public Task<int> DeleteUserById(Guid Id);

        #endregion
    }
}
