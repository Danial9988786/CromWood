using Azure;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.ViewModels;

namespace CromWood.Business.Services.Interface
{
    public interface IRolePermissionService
    {
        public Task<AppResponse<IEnumerable<PermissionModel>>> GetPermissionsAsync();
        public Task<AppResponse<IEnumerable<RoleViewModel>>> GetRolesAsync();
        public Task<AppResponse<string>> AddRoleAsync(RoleModel role);
        public Task<AppResponse<string>> EditRoleAsync(RoleModel role);
        public Task<AppResponse<RoleModel>> GetRoleByIdAsync(Guid Id);
        public Task<AppResponse<string>> DeleteRoleAync(Guid Id);
    }
}
