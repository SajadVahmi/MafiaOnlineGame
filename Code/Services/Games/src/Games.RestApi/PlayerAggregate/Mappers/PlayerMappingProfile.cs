using AutoMapper;
using Games.Application.PlayerAggregate.Commands.ChangePlayerGender;
using Games.Application.PlayerAggregate.Commands.RegisterPlayer;
using Games.Application.PlayerAggregate.Commands.RenamePlayer;
using Games.Contract.PlayerAggregate.Dto;
using Games.Domain.PlayerAggregate.Models;
using Games.Query.PlayerAggregate.Queries.ViewProfile;
using Games.RestApi.PlayerAggregate.Requests;
using Games.RestApi.PlayerAggregate.Responses;

namespace Games.RestApi.PlayerAggregate.Mappers;

public class PlayerMappingProfile:Profile
{
    public PlayerMappingProfile()
    {
        CreateMap<Player, RegisteredPlayerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.LastName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id.ToString()));

        CreateMap<RegisterPlayerRequest, RegisterPlayerCommand>();
        CreateMap<RegisteredPlayerDto, RegisteredPlayerResponse>();

        CreateMap<RenamePlayerRequest, RenamePlayerCommand>();
        CreateMap<ChangePlayerGenderRequest, ChangePlayerGenderCommand>();

        CreateMap<ViewProfileQueryResult, ViewProfileResponse>();
    }
}