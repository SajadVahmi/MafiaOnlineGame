using Players.Contracts.Enums;

namespace Players.RestApi.V1.PlayerAggregate.Requests.Register;

public class PlayerRegistrationRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public Gender? Gender { get; set; }


}
