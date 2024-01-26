using AutoMapper;
using IDP.Administration.Api.V1.Users.Models;
using IDP.Administration.Services.Users.Dto;

namespace IDP.Administration.Api.V1.Users.Mappers;

public class UserMapper:Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserRequestBody, CreateUserDto>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Mobile));

            
    }
}