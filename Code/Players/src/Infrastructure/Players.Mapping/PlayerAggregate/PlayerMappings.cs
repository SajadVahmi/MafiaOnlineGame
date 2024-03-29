﻿using AutoMapper;
using Players.ApplicationServices.PlayerAggregate.Dto;
using Players.Domain.PlayerAggregate.Models;
using Players.RestApi.V1.PlayerAggregate.Requests.ChangeProfile;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;
using Players.RestApi.V1.PlayerAggregate.Responses.Register;

namespace Players.Mapping.PlayerAggregate;

public class PlayerMappings:Profile
{
    public PlayerMappings()
    {
        CreateMap<Player, RegisteredPlayerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

        CreateMap<PlayerRegistrationRequest, PlayerRegistrationDto>();

        CreateMap<RegisteredPlayerDto, PlayerRegistrationResponse>();

        CreateMap<PlayerChangeProfileRequest, PlayerChangeProfileDto>();

        CreateMap<Player, PlayerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
    }
}
