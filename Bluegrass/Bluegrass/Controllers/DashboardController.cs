using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
