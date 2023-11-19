﻿using Players.Contracts.Enums;

namespace Players.ApplicationServices.PlayerAggregate.Dtos;

public class PlayerRegistrationDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly BirthDate { get; set; }

    public Gender Gender { get; set; }

    public required string UserId { get; set; }
}
