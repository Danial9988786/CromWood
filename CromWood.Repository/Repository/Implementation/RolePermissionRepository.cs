using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class RolePermissionRepository : Repository<Role>, IRolePermissionRepository
    {
        public RolePermissionRepository(CromwoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Permission>> GetPermission()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.Include(x => x.Users).ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(Guid Id)
        {
            return await _context.Roles.Include(x => x.Permissions).ThenInclude(y => y.Permission).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> AddRole(Role role)
        {
            try
            {
                role.Id = Guid.NewGuid();
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> EditRole(Role role)
        {
            try
            {
                _context.Roles.Update(role);
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
