using System.Collections.Generic;
using System.Threading.Tasks;
using Bluegrass.Shared.DTO.Helper;
using Bluegrass.Shared.DTO.Helper;
using Bluegrass.Shared.DTO.Person;

namespace Bluegrass.Core.Services.HelperService
{
    public interface IHelperService
    {
        Task<IEnumerable<DropdownCountryDTO>> ListCountriesAsync();
        Task<IEnumerable<DropdownGenderDTO>> ListGendersAsync();
    }
}