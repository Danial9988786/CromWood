using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly IAuthService authService;
        public TestController(ITestService testService, IAuthService authService)
        {
            this.testService = testService;
            this.authService = authService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Auth()
        {
            var user = authService.GetCurrentUser(HttpContext.User);
            return View(user);
        }
    }
}
