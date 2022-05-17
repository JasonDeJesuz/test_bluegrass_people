using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View("Persons");
        }
        public IActionResult Create()
        {
            return View("Person");
        }
        public IActionResult Edit()
        {
            return View("Person");
        }
    }
}
