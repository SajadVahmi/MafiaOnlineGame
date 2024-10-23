using Games.Contract._Shared.Enums;

namespace Games.Contract.PlayerAggregate.Dto;

public class RegisteredPlayerDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Gender Gender { get; set; }
    public string UserId { get; set; } = null!;
}