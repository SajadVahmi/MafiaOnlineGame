﻿using Framework.Core.Application.Commands;
using Games.Contract._Shared.Enums;

namespace Games.Application.PlayerAggregate.Commands.ChangePlayerGender;

public class ChangePlayerGenderCommand : ICommand
{
    public string PlayerId { get; set; } = null!;
    public Gender Gender { get; set; }
}

