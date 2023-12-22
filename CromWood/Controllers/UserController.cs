using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// This method will list the users and display in respective tab.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.ActionDone = TempData["action"];
            ViewBag.UserName = TempData["userName"];
            var result = await _userService.GetAllUsersAsync();
            return View(result.Data);
        }

        /// <summary>
        /// This method is used to show modal to invite user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InviteUser()
        {
            return PartialView();
        }

        /// <summary>
        /// This method is used to send invitation to user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InviteUser([FromForm]UserModel user)
        {
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
        public IActionResult BlockUserModal(Guid Id)
        {
            return PartialView("BlockUser", Id);
        }

        /// <summary>
        /// POST: This method will take action as a post request to block user.
        /// </summary>
        public async Task<IActionResult> BlockUser(Guid Id)
        {
            await _userService.BlockUserById(Id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: This method will show modal to confirm user deletion
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DeleteUserModal(Guid Id)
        {
            var result = await _userService.GetUserById(Id);
            ViewBag.Name = result.Data.Name;
            return PartialView("DeleteUser", Id);
        }

        /// <summary>
        /// POST: This method will take action as a post request to delete user.
        /// </summary>
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var result = await _userService.DeleteUserById(Id);
            TempData["action"] = "deleted";
            TempData["userName"] = result.Data;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeRole(Guid userId, Guid roleId)
        {
            await _userService.ChangeUserRole(userId, roleId);
            return RedirectToAction("Index");
        }
    }
}
