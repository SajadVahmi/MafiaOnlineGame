using Players.Contracts.Enums;

namespace Players.RestApi.V1.PlayerAggregate.Requests.ChangeProfile;

public class PlayerChangeProfileRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public Gender? Gender { get; set; }
}
