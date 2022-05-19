using AutoMapper;
using Bluegrass.Data.Data.Models;
using Bluegrass.Shared.DTO.Address;
using Bluegrass.Shared.DTO.Helper;

namespace Bluegrass.Profiles
{
    public class HelperProfile : Profile
    {
        public HelperProfile()
        {
            CreateMap<DropdownCountry, DropdownCountryDTO>().ReverseMap();

            CreateMap<DropdownGender, DropdownGenderDTO>().ReverseMap();
        }
    }
}