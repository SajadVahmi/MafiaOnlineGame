using Framework.Core.Application.Commands;

namespace Games.Application.PlayerAggregate.Commands.RenamePlayer;

public class RenamePlayerCommand : ICommand
{
    public required string PlayerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}