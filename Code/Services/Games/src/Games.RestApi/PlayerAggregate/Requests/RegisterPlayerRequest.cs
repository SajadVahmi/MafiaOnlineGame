using Games.Contract.Enums;

namespace Games.RestApi.PlayerAggregate.Requests;

public class RegisterPlayerRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
}