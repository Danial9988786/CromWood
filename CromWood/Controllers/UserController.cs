using CromWood.Business.Constants;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using CromWood.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        /// <summary>
        /// This method will list the users and display in respective tab.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.ViewAll);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            ViewBag.ActionDone = TempData["action"];
            ViewBag.UserName = TempData["userName"];
            var result = await _userService.GetAllUsersAsync();
            return View(result.Data);
        }

        /// <summary>
        /// This method is used to show modal to invite user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> InviteUser()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            return PartialView();
        }

        /// <summary>
        /// This method is used to send invitation to user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InviteUser([FromForm]UserModel user)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }

            user.IsActive = true;
            user.Id = Guid.NewGuid();
            await _userService.InviteUser(user);
            TempData["action"] = "invited";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: This method will show modal to confirm block user
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> BlockUserModal(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _userService.GetUserById(Id);
            ViewBag.Name = result.Data.Name;
            return PartialView("BlockUser", Id);
        }

        /// <summary>
        /// POST: This method will take action as a post request to block user.
        /// </summary>
        public async Task<IActionResult> BlockUser(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _userService.BlockUserById(Id);
            TempData["action"] = "blocked";
            TempData["userName"] = result.Data;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: This method will show modal to confirm user deletion
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DeleteUserModal(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.CanDelete);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _userService.GetUserById(Id);
            ViewBag.Name = result.Data.Name;
            return PartialView("DeleteUser", Id);
        }

        /// <summary>
        /// POST: This method will take action as a post request to delete user.
        /// </summary>
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.CanDelete);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _userService.DeleteUserById(Id);
            TempData["action"] = "deleted";
            TempData["userName"] = result.Data;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// POST: This method will take action as a post request to change user role.
        /// </summary>
        public async Task<IActionResult> ChangeRole(Guid userId, Guid roleId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.UserManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            await _userService.ChangeUserRole(userId, roleId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProfileSetting()
        {
            var user = await _authService.GetLoggedInUser();
            return View(user.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFirstName(string firstname)
        {
            var result = await _userService.UpdateFirstName(firstname);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLastName(string lastname)
        {
            var result = await _userService.UpdateLastName(lastname);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhone(string phone)
        {
            var result = await _userService.UpdatePhone(phone);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(IFormFile file)
        {
            var result = await _userService.UpdateAvatar(file);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Your password doesn't meet criteria");
            }
            var result = await _userService.UpdatePassword(model);
            return StatusCode(result.StatusCode, result.Message);
        }
        
    }
}
