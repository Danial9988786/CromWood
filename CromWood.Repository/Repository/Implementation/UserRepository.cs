using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CromwoodContext context) : base(context)
        {
        }

        public bool CheckEmail(string email)
        {
            var user = _context.Users.Any(x => x.Email == email);
            if (user == true)
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetUser(string email, string password)
        {
            var currentUser = await _context.Users.Include(x => x.Role)
                .ThenInclude(x => x.Permissions)
                .ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(o => o.Email.ToLower() == email.ToLower() && o.Password == password);

            return currentUser;
        }
    }
}
