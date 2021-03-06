using Bluegrass.Core.Services.HelperService;
using Bluegrass.Core.Services.PersonService;
using Bluegrass.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Controllers
{
    public class PersonController : Controller
    {
        #region Members
        private readonly IPersonService _personService;
        private readonly IHelperService _helperService;
        #endregion
        #region Constructor
        public PersonController(IPersonService personService, IHelperService helperService)
        {
            _personService = personService;
            _helperService = helperService;
        }
        #endregion
        public async Task<IActionResult> Index( string actionType, int id = 0)
        {
            var thisPersonViewModel = new PersonViewModel()
            {
                Id = id,
                ActionType = actionType,
                Person = !string.IsNullOrEmpty(actionType) && actionType.Equals("modify") ? await _personService.GetAsync(id) : new Shared.DTO.Person.PersonDTO(),
                Countries = await _helperService.ListCountriesAsync(),
                Genders = await _helperService.ListGendersAsync()
            };

            return View("Person", thisPersonViewModel);
        }
        public IActionResult List()
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
