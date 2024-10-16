using Games.Domain.Contracts.Enums;

namespace Games.RestApi.PlayerAggregate.Responses;

public class ViewProfileResponse
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
}