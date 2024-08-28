using Framework.Core.Application.Commands;
using Games.Domain.Contracts.Enums;

namespace Games.Application.PlayerAggregate.Commands;

public class RegisterPlayerCommand:ICommand
{
    public required string Name { get; set; }
    public required string Family { get; set; }
    public Gender Gender { get; set; }
}