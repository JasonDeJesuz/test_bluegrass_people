using Bluegrass.Core.Services.UserAuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToActionPermanent("List", "Person");
        }
    }
}
