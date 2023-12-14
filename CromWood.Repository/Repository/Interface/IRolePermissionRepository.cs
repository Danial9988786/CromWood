using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IRolePermissionRepository: IRepository<Role>
    {
        public Task<IEnumerable<Permission>> GetPermission();
        public Task<IEnumerable<Role>> GetRoles();
        public Task<Role> GetRoleByIdAsync(Guid Id);
        public Task<string> AddRole(Role role);
    }
}
