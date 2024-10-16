using Framework.Core.Application.Commands;
using Games.Application.PlayerAggregate.Dto;
using Games.Domain.Contracts.Enums;

namespace Games.Application.PlayerAggregate.Commands.RegisterPlayer;

public class RegisterPlayerCommand : ICommand<RegisteredPlayerDto>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
}