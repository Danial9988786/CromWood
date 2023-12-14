using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class RolePermissionController : Controller
    {
        private readonly IRolePermissionService _rolePermissionService;
        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        /// <summary>
        /// This method will list the role in role and permission view.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var roles = await _rolePermissionService.GetRolesAsync();
            return View(roles.Data);
        }

        /// <summary>
        /// GET: This method will provide view for adding or modifying role.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddModifyRole([FromRoute] Guid Id)
        {
            var role = new RoleModel();
            if (Id == Guid.Empty)
            {
                var permissions = await _rolePermissionService.GetPermissionsAsync();
                var rolePermissions = new List<RolePermissionModel>();
                foreach (var permission in permissions.Data.Where(x => x.PermissionKey != "all_permission"))
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
                role = result.Data;
            }

            return PartialView(role);
        }

        /// <summary>
        /// POST: This method will take action on posting role for add or edit.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddModifyRole([FromForm] RoleModel role)
        {
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
        /// POST: This method will take action as a post request to delete role.
        /// </summary>
        /// <returns></returns>
        [Route("Delete/{Id}")]
        public async Task<IActionResult> DeleteRole(Guid Id)
        {
            await _rolePermissionService.DeleteRoleAync(Id);
            return RedirectToAction("Index", "RolePermission");
        }
    }
}
