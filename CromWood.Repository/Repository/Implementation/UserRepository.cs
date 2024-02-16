using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

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

        public async Task<User> GetUser(Guid Id)
        {
            var currentUser = await _context.Users
                .FirstOrDefaultAsync(o => o.Id==Id);
            return currentUser;
        }

        public async Task<User> GetUser(string email)
        {
            var currentUser = await _context.Users
                .FirstOrDefaultAsync(o => o.Email == email);
            return currentUser;
        }

        public async Task<List<RolePermission>> GetAllRolesAndPermission()
        {
            return await _context.RolePermissions.Include(x => x.Permission).ToListAsync();
        }


        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.OrderByDescending(x=>x.CreatedDate).ToListAsync();
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
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> BlockUserById(Guid Id)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                user.IsActive = false;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user.FirstName + " " + user.LastName;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> SetUserActive(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> SetUserInactive(Guid id)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                user.LastActive = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteUserById(Guid Id)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user.FirstName + " " + user.LastName;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> SetOTPForUser(string email, string otp)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                user.OTP = otp;
                user.OTPExpirationDate = DateTime.Now.AddMinutes(10);
                await _context.SaveChangesAsync();
                return user.Email;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateUserPassword(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return "success";
            }
            catch
            {
                throw;
            }
        }
    }
}
