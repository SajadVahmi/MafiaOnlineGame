using Games.Domain.Contracts.Enums;

namespace Games.Query.PlayerAggregate.Models;

public class PlayerQueryModel
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
    public string? UserId { get; set; }
}