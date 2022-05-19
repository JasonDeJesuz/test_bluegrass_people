using Bluegrass.Shared.DTO.Helper;

namespace Bluegrass.Models
{
    public class HelpersViewModel
    {
        public IEnumerable<DropdownCountryDTO>? Countries { get; set; }
        public IEnumerable<DropdownGenderDTO>? Genders { get; set; }
    }
}