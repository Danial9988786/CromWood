using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
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
            var result = await testService.GetDashboardOverview();
            return View(result.Data);
        }

        public async Task<IActionResult> OverviewJSON()
        {
            var result = await testService.GetDashboardOverviewJSON();
            return StatusCode(result.StatusCode,result.Data);
        }

        [Authorize]
        public IActionResult Auth()
        {
            var user = authService.GetCurrentUser(HttpContext.User);
            return View(user);
        }
    }
}
