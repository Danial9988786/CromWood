using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace CromWood.Business.Services.Implementation
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _roleRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public RolePermissionService(IRolePermissionRepository roleRepo, IMapper mapper, IMemoryCache cache)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<AppResponse<IEnumerable<PermissionModel>>> GetPermissionsAsync()
        {
            try
            {
                var permissions = await _roleRepo.GetPermission();
                var mappedPermissions = _mapper.Map<List<PermissionModel>>(permissions);
                return ResponseCreater<IEnumerable<PermissionModel>>.CreateSuccessResponse(mappedPermissions, "Permissions loaded successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<PermissionModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<RoleViewModel>>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleRepo.GetRoles();
                var mappedRoles = _mapper.Map<List<RoleViewModel>>(roles);
                return ResponseCreater<IEnumerable<RoleViewModel>>.CreateSuccessResponse(mappedRoles, "Roles loaded successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<RoleViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> AddRoleAsync(RoleModel role)
        {
            try
            {
                var mappedRole = _mapper.Map<Role>(role);
                mappedRole.Permissions.ToList().ForEach(x => x.Permission = null);
                await _roleRepo.AddRole(mappedRole);

                // If new Role is added, role_permission cache is cleared to keep this upto date.
                _cache.Remove("role_permission");
                return ResponseCreater<string>.CreateSuccessResponse(null, "Roles added successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<RoleModel>> GetRoleByIdAsync(Guid Id)
        {
            try
            {
                var roles = await _roleRepo.GetRoleByIdAsync(Id);
                var mappedRoles = _mapper.Map<RoleModel>(roles);
                return ResponseCreater<RoleModel>.CreateSuccessResponse(mappedRoles, "Roles loaded successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<RoleModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> EditRoleAsync(RoleModel role)
        {
            try
            {
                var mappedRole = _mapper.Map<Role>(role);
                mappedRole.Permissions.ToList().ForEach(x => x.Permission = null);
                await _roleRepo.EditRole(mappedRole);
                // If new Role is edited, role_permission cache is cleared to keep this upto date.
                _cache.Remove("role_permission");
                return ResponseCreater<string>.CreateSuccessResponse(null, "Roles added successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<string>> DeleteRoleAync(Guid Id)
        {
            try
            {
                await _roleRepo.DeleteAsync(Id);
                // If new Role is deleted, role_permission cache is cleared to keep this upto date.
                _cache.Remove("role_permission");
                return ResponseCreater<string>.CreateSuccessResponse(null, "Roles deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

    }
}
