using Games.Contract.Enums;

namespace Games.RestApi.PlayerAggregate.Responses;

public class RegisteredPlayerResponse
{
    public string? Id { get; set; } = null!;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public Gender? Gender { get; set; }
}