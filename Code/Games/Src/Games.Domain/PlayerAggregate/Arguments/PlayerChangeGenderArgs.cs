using Framework.Core.Contracts;
using Games.Domain.Contracts.Enums;

namespace Games.Domain.PlayerAggregate.Arguments;

public class PlayerChangeGenderArgs
{
    public Gender Gender { get; set; }
    public required IIdGenerator IdGenerator { get; set; }
    public required IClock Clock { get; set; }
}