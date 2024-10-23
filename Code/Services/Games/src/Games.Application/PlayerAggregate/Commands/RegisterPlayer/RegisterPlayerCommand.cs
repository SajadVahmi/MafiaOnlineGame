using Framework.Core.Application.Commands;
using Games.Contract._Shared.Enums;
using Games.Contract.PlayerAggregate.Dto;

namespace Games.Application.PlayerAggregate.Commands.RegisterPlayer;

public class RegisterPlayerCommand : ICommand<RegisteredPlayerDto>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
}