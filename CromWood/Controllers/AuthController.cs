using CromWood.Business.Services.Interface;
using CromWood.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = "/")
        {
            return View(new LoginModel() { ReturnUrl = ReturnUrl});
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult NotAuthorizedJSON()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel login)
        {
            var loginResponse = await authService.Login(login);
            if (loginResponse.Data == null)
            {
                ModelState.AddModelError(string.Empty, loginResponse.Message);
                return View(login);
            }
            else
            {
                if (login.ReturnUrl != null)
                {
                    return Redirect(login.ReturnUrl);
                }
                return RedirectToAction("Auth", "Test");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await authService.Logout();
            return RedirectToAction("Auth", "Test");
        }
    }
}
