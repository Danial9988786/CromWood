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
        public async Task<IActionResult> Index()
        {
            var tests = await testService.GetTests();
            return View(tests);
        }

        [Authorize]
        public IActionResult Auth()
        {
            var user = authService.GetCurrentUser(HttpContext.User);
            return View(user);
        }
    }
}
