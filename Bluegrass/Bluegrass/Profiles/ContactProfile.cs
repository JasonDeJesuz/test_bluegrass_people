using AutoMapper;
using Bluegrass.Data.Data.Models;
using Bluegrass.Shared.DTO.Contact;

namespace Bluegrass.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();
        }
    }
}