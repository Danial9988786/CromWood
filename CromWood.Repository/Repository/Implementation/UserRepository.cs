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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<int> InviteUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
               throw;
            }
        }
        public async Task<int> ChangeUserRole(Guid UserId, Guid RoleId)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == UserId);
                user.RoleId = RoleId;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> BlockUserById(Guid Id)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                user.IsActive = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteUserById(Guid Id)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
