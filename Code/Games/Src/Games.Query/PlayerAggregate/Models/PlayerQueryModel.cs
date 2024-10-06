using Games.Domain.Contracts.Enums;

namespace Games.Query.PlayerAggregate.Models;

public class PlayerQueryModel
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Gender Gender { get; private set; }
    public string UserId { get; set; } = null!;
}