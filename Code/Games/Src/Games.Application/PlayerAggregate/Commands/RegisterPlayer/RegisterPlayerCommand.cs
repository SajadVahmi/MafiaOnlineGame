﻿using Framework.Core.Application.Commands;
using Games.Domain.Contracts.Enums;

namespace Games.Application.PlayerAggregate.Commands.RegisterPlayer;

public class RegisterPlayerCommand : ICommand
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
}