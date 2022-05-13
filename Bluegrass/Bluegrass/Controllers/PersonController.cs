using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View("Persons");
        }
    }
}
