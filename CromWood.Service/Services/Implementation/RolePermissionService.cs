using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using System.Data;

namespace CromWood.Business.Services.Implementation
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _roleRepo;
        private readonly IMapper _mapper;
        public RolePermissionService(IRolePermissionRepository roleRepo, IMapper mapper)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
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
                return ResponseCreater<string>.CreateSuccessResponse(null, "Roles deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

    }
}
