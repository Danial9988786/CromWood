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
            return View(new LoginModel() { ReturnUrl = ReturnUrl, RememberMe = true });
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
                return RedirectToAction("Index", "Test");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await authService.Logout();
            return RedirectToAction("Index", "Test");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            var result = await authService.SendOTP(model);
            if (result)
            {
                // Sucessfully send OTP
                TempData["UserEmail"] = model.Email;
                return RedirectToAction("VerifyOTP", model);
            }
            else
            {
                ModelState.AddModelError("OTPFAILED", "Sorry, we were unable to send OTP.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult VerifyOTP()
        {
            var email = TempData["UserEmail"]?.ToString();
            if(email == null)
            {
                return RedirectToAction("ForgotPassword");
            }
            var model = new ForgotPasswordModel()
            {
                Email = email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOTP(ForgotPasswordModel model)
        {
            var result = await authService.VerifyOTP(model);
            if (result)
            {
                return View("ResetPassword", new ResetPasswordModel() { Email = model.Email, OTP= model.OTP});
            }
            else
            {
                ModelState.AddModelError("INVALIDOTP", "The OTP is not valid or Expired");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Password doesn't met criteria");
                return View(model);
            }
            var resetResponse = await authService.ResetPassword(model);
            if (resetResponse.Data == null)
            {
                ModelState.AddModelError(string.Empty, resetResponse.Message);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}
