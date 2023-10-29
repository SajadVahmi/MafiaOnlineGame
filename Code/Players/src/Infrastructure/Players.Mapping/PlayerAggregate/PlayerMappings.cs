using AutoMapper;
using Players.ApplicationServices.PlayerAggregate.Dtos;
using Players.Domain.PlayerAggregate.Models;

namespace Players.Mapping.PlayerAggregate;

public class PlayerMappings:Profile
{
    public PlayerMappings()
    {
        CreateMap<Player, RegisteredPlayerDto>();
    }
}
