using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IDP.Administration.Api.Users.Models;
using IDP.Administration.Services.Users.Dtos;
using IDP.Shared.IdentityStore.Models;

namespace IDP.Administration.Api.Users.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<CreateUserRequestBody, CreateUserDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Mobile));


        }
    }
}
