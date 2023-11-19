using Players.Contracts.Enums;

namespace Players.RestApi.V1.PlayerAggregate.Responses.Register;

public class PlayerRegisterationResponse
{
    public required string Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly BirthDate { get; set; }

    public Gender Gender { get; set; }

    public DateTimeOffset RegisterDateTime { get; set; }
}
