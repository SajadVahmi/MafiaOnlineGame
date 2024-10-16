using Games.Domain.Contracts.Enums;

namespace Games.RestApi.PlayerAggregate.Requests;

public class RegisterPlayerRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
}