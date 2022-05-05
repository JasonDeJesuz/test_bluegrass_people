using Bluegrass.Shared.DTO.Person;
using AutoMapper;
using Bluegrass.Data.Data.Models;

namespace Bluegrass.Shared.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDTO>()
                .ForMember(
                    dest => dest.Address,
                    opt => opt.MapFrom(src => src.Address)
                )
                .ForMember(
                    dest => dest.Contact,
                    opt => opt.MapFrom(src => src.Contact)
                )
                .ForMember(
                    dest => dest.ProfilePicture,
                    opt => opt.MapFrom(src => src.ProfilePicture)
                );
            CreateMap<PersonDTO, Person>();
        }
    }
}