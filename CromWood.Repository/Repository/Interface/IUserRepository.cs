using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<User> GetUser(string email, string password);
        public bool CheckEmail(string email);
    }
}
