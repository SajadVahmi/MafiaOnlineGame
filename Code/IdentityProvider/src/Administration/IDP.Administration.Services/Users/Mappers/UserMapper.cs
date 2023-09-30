using AutoMapper;
using IDP.Administration.Services.Users.Dtos;
using IDP.Shared.IdentityStore.Models;

namespace IDP.Administration.Services.Users.Mappers
{
    public class UserMapper:Profile
    {
        public UserMapper()
        {
            CreateMap<CreateUserDto, IdpUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
