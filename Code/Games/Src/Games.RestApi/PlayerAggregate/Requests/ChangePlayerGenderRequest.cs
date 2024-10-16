using Games.Domain.Contracts.Enums;

namespace Games.RestApi.PlayerAggregate.Requests;

public class ChangePlayerGenderRequest
{
    public Gender? Gender { get; set; }
}