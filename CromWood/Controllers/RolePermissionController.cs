using CromWood.Business.Constants;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
    public class RolePermissionController : Controller
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IAuthService _authService;
        public RolePermissionController(IRolePermissionService rolePermissionService, IAuthService authService)
        {
            _rolePermissionService = rolePermissionService;
            _authService = authService;
        }

        /// <summary>
        /// This method will list the role in role and permission view.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.RoleManagement, PermissionConstant.ViewAll);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var roles = await _rolePermissionService.GetRolesAsync();
            return View(roles.Data);
        }

        /// <summary>
        /// GET: This method will provide view for adding or modifying role.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> AddModifyRole(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.RoleManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }

            var role = new RoleModel();
            var permissions = await _rolePermissionService.GetPermissionsAsync();

            if (Id == Guid.Empty)
            {
                var rolePermissions = new List<RolePermissionModel>();
                foreach (var permission in permissions.Data)
                {
                    rolePermissions.Add(new RolePermissionModel()
                    {
                        Permission = permission,
                    });
                }
                role.RolePermission = rolePermissions;
            }
            else
            {
                var result = await _rolePermissionService.GetRoleByIdAsync(Id);
                foreach (var permission in permissions.Data)
                {
                    if (!result.Data.RolePermission.Where(x => x.Permission.PermissionKey == permission.PermissionKey).Any())
                    {
                        result.Data.RolePermission.Add(new RolePermissionModel()
                        {
                            Permission = permission,
                        });
                    }
                }
                role = result.Data;
            }

            return PartialView(role);
        }

        /// <summary>
        /// POST: This method will take action on posting role for add or edit.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddModifyRole([FromForm] RoleModel role)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.RoleManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }

            if (role.Id == Guid.Empty)
            {
                await _rolePermissionService.AddRoleAsync(role);
            }
            else
            {
                await _rolePermissionService.EditRoleAsync(role);
            }
            return RedirectToAction("Index", "RolePermission");
        }

        /// <summary>
        /// GET: This method will show modal to confirm role deletion
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DeleteRoleModal(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.RoleManagement, PermissionConstant.CanDelete);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            return PartialView("DeleteRole", Id);
        }

        /// <summary>
        /// POST: This method will take action as a post request to delete role.
        /// </summary>
        public async Task<IActionResult> DeleteRole(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.RoleManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            await _rolePermissionService.DeleteRoleAync(Id);
            return RedirectToAction("Index");
        }
    }
}
