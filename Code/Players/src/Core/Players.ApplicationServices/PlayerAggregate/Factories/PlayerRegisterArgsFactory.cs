﻿using Framework.Core.Contracts;
using Players.ApplicationServices.PlayerAggregate.Dto;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;

namespace Players.ApplicationServices.PlayerAggregate.Factories;

public static class PlayerRegisterArgsFactory
{
    public static async Task<PlayerRegisterArgs> CreateAsync(PlayerRegistrationDto playerRegistrationDto,
        IPlayerRepository playerRepository,
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService,
        IEventIdProvider eventIdProvider,
        IClock clock,
        CancellationToken cancellationToken=default)
    {

        PlayerRegisterArgs playerRegisterArgs = new()
        {
            Id = await playerRepository.GetNextIdAsync(cancellationToken),

            FirstName = playerRegistrationDto.FirstName,

            LastName = playerRegistrationDto.LastName,

            BirthDate = playerRegistrationDto.BirthDate,

            Gender = playerRegistrationDto.Gender,

            UserId = playerRegistrationDto.UserId,

            DuplicateRegistrationCheckService = duplicateRegistrationCheckService,

            IdProvider = eventIdProvider,

            Clock = clock,
        };

        return playerRegisterArgs;
    }

}
