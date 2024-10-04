using Framework.Core.Application.Commands;
using Games.Domain.Contracts.Enums;

namespace Games.Application.PlayerAggregate.Commands.ChangePlayerGender;

public class ChangePlayerGenderCommand : ICommand
{
    public required string PlayerId { get; set; }
    public Gender Gender { get; set; }
}