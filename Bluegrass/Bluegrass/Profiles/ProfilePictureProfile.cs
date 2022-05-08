using AutoMapper;
using Bluegrass.Data.Data.Models;
using Bluegrass.Shared.DTO.ProfilePicture;

namespace Bluegrass.Profiles
{
    public class ProfilePictureProfile : Profile
    {
        public ProfilePictureProfile()
        {
            CreateMap<ProfilePicture, ProfilePictureDTO>();
            CreateMap<ProfilePictureDTO, ProfilePicture>();
        }
    }
}