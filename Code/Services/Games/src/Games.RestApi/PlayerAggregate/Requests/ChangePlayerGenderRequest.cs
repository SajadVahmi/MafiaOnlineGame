using Games.Contract.Enums;

namespace Games.RestApi.PlayerAggregate.Requests;

public class ChangePlayerGenderRequest
{
    public Gender? Gender { get; set; }
}