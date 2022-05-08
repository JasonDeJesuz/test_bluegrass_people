using AutoMapper;
using Bluegrass.Data.Data.Models;
using Bluegrass.Shared.DTO.Address;

namespace Bluegrass.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
        }
    }
}