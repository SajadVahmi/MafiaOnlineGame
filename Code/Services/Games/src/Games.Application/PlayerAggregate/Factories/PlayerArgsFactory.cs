﻿using Framework.Core.ServiceContracts;
using Games.Application.PlayerAggregate.Commands.ChangePlayerGender;
using Games.Application.PlayerAggregate.Commands.RegisterPlayer;
using Games.Application.PlayerAggregate.Commands.RenamePlayer;
using Games.Domain.PlayerAggregate.Arguments;
using Games.Domain.PlayerAggregate.Contracts;

namespace Games.Application.PlayerAggregate.Factories;

public static class PlayerArgsFactory
{
    public static PlayerRegistrationArgs CreateRegistrationArgs(
        RegisterPlayerCommand command,
        IIdGenerator idGenerator,
        IPlayerDuplicationRegistrationDetector duplicationRegistrationDetector,
        IAuthenticatedUser authenticatedUser,
        IClock clock)
    {
        return new PlayerRegistrationArgs()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Gender = command.Gender,
            UserId = authenticatedUser.GetSub()!,
            Clock = clock,
            DuplicationRegistrationDetector = duplicationRegistrationDetector,
            IdGenerator = idGenerator,
            
        };
    }

    public static PlayerRenameArgs CreateRenameArgs(RenamePlayerCommand command, IIdGenerator idGenerator,IClock clock)
    {
        return new PlayerRenameArgs()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Clock = clock,
            IdGenerator = idGenerator
        };
    }

    public static PlayerChangeGenderArgs CreateChangeGenderArgs(ChangePlayerGenderCommand command, IIdGenerator idGenerator,IClock clock)
    {
        return new PlayerChangeGenderArgs()
        {
            Gender = command.Gender,
            Clock = clock,
            IdGenerator = idGenerator
        };
    }
}