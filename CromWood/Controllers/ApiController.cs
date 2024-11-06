using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
