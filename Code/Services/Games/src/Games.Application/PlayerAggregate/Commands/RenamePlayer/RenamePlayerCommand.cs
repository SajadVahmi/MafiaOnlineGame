using Framework.Core.Application.Commands;

namespace Games.Application.PlayerAggregate.Commands.RenamePlayer;

public class RenamePlayerCommand : ICommand
{
    public string PlayerId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}