using AutoMapper;
using Bluegrass.Data.Authentication;
using Bluegrass.Shared.DTO.Auth;

namespace Bluegrass.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationDto, ApplicationUser>();
        }
    }
}
