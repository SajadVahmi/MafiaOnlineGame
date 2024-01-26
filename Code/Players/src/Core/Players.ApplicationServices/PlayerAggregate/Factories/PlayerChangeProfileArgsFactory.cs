using Framework.Core.Contracts;
using Players.ApplicationServices.PlayerAggregate.Dto;
using Players.Domain.PlayerAggregate.Models;

namespace Players.ApplicationServices.PlayerAggregate.Factories;

public static class PlayerChangeProfileArgsFactory
{
    public static  PlayerChangeProfileArgs Create(PlayerChangeProfileDto playerChangeProfileDto,
        IEventIdProvider eventIdProvider,
        IClock clock)
    {

        PlayerChangeProfileArgs playerChangeProfileArgs = new()
        {

            FirstName = playerChangeProfileDto.FirstName,

            LastName = playerChangeProfileDto.LastName,

            BirthDate = playerChangeProfileDto.BirthDate,

            Gender = playerChangeProfileDto.Gender,

            IdProvider = eventIdProvider,

            Clock = clock,
        };

        return playerChangeProfileArgs;
    }

}