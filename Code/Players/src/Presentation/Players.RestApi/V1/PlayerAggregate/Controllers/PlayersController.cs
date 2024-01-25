using FluentValidation;
using FluentValidation.Results;
using Framework.Core.ApplicationServices.Exceptions;
using Framework.Core.Contracts;
using Framework.Presentation.RestApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Players.ApplicationServices.PlayerAggregate.Dto;
using Players.ApplicationServices.PlayerAggregate.Services;
using Players.Domain.PlayerAggregate.Exceptions;
using Players.RestApi.V1.PlayerAggregate.Requests.ChangeProfile;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;
using Players.RestApi.V1.PlayerAggregate.Responses.Register;
using Players.RestApi.V1.Shared;
using System;

namespace Players.RestApi.V1.PlayerAggregate.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class PlayersController(
    IPlayerApplicationService playerApplicationService,
    IMapperAdapter mapper,
    IAuthenticatedUser authenticatedUser)
    : ControllerBase
{
    [HttpGet("{playerId}")]
    [ProducesResponseType(type:typeof(PlayerDto),statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ViewAsync(long playerId, CancellationToken cancellationToken = default)
    {
        try
        {
            var player = await playerApplicationService.ViewAsync(
                playerId: playerId,
                userId: authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken),
                cancellationToken: cancellationToken);

            return Ok(player);
        }
        catch (NotFoundException exception)
        {
              return NotFound(ApiError.Instantiate(exception));
        }


    }

    [HttpPost]
    [ProducesResponseType(type: typeof(PlayerRegisterationResponse), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status409Conflict)]
    [ProducesResponseType(type: typeof(IEnumerable<ApiValidationError>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] PlayerRegistrationRequest request, [FromServices] IValidator<PlayerRegistrationRequest> validator)
    {

        var requestValidationResult = validator.Validate(request);

        if (!requestValidationResult.IsValid)
            return BadRequest(ApiValidationError.Instantiate(requestValidationResult));

        var playerForRegister =
       mapper.Map<PlayerRegistrationRequest, PlayerRegistrationDto>(request);

        playerForRegister.UserId = authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken);

        RegisteredPlayerDto registeredPlayer;

        try
        {
            registeredPlayer = await playerApplicationService.RegisterAsync(playerForRegister);
        }
        catch (TheUserAlreadyRegistredException exception)
        {
            return Conflict(ApiError.Instantiate(exception));
        }

        var playerRegistrationResponse =
            mapper.Map<RegisteredPlayerDto, PlayerRegisterationResponse>(registeredPlayer);

        return Created(string.Empty, playerRegistrationResponse);

    }

    [HttpPut("{playerId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(type: typeof(IEnumerable<ApiValidationError>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeProfileAsync([FromRoute] long playerId,[FromBody] PlayerChangeProfileRequest request, [FromServices] IValidator<PlayerChangeProfileRequest> validator)
    {
        var requestValidationResult = validator.Validate(request);

        if (!requestValidationResult.IsValid)
            return BadRequest(ApiValidationError.Instantiate(requestValidationResult));

        var playerChangeProfileDto =
       mapper.Map<PlayerChangeProfileRequest, PlayerChangeProfileDto>(request);

        playerChangeProfileDto.Id = playerId;
        
        playerChangeProfileDto.UserId = authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken);

        try
        {
            await playerApplicationService.ChangeProfileAsync(playerChangeProfileDto);
        }
        catch (NotFoundException exception)
        {
            return NotFound(ApiError.Instantiate(exception));
        }

        return NoContent();

    }

}

