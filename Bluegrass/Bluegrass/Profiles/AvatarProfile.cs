using AutoMapper;
using Bluegrass.Data.Data.Models;
using Bluegrass.Shared.DTO.Avatar;

namespace Bluegrass.Profiles
{
    public class AvatarProfile : Profile
    {
        public AvatarProfile()
        {
            CreateMap<Avatar, AvatarDTO>();
            CreateMap<AvatarDTO, Avatar>();
        }
    }
}