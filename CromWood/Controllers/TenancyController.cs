using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
    public class TenancyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
