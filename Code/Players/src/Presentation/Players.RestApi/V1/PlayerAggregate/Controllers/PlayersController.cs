using FluentValidation;
using Framework.Core.Contracts;
using Framework.Presentation.RestApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Players.ApplicationServices.PlayerAggregate.Dto;
using Players.ApplicationServices.PlayerAggregate.Services;
using Players.Contracts.Resources;
using Players.RestApi.V1.PlayerAggregate.Requests.ChangeProfile;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;
using Players.RestApi.V1.PlayerAggregate.Responses.Register;
using Players.RestApi.V1.Shared;

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
    [ProducesResponseType(type: typeof(PlayerDto), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ViewAsync(long playerId, CancellationToken cancellationToken = default)
    {
        var player = await playerApplicationService.ViewAsync(
            playerId: playerId,
            userId: authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken),
            cancellationToken: cancellationToken);

        return Ok(player);

    }

    [HttpPost]
    [ProducesResponseType(type: typeof(PlayerRegistrationResponse), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status409Conflict)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] PlayerRegistrationRequest request, [FromServices] IValidator<PlayerRegistrationRequest> validator)
    {
        var requestValidationResult = validator.Validate(request);

        if (!requestValidationResult.IsValid)
            return BadRequest(ApiError.Instantiate(
                code: PlayersCodes.RequestValidation400,
                message: PlayersResource.RequestValidation400,
                requestValidationResult));

        var playerForRegister =
           mapper.Map<PlayerRegistrationRequest, PlayerRegistrationDto>(request);

        playerForRegister.UserId = authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken);

        var registeredPlayer = await playerApplicationService.RegisterAsync(playerForRegister);

        var playerRegistrationResponse =
            mapper.Map<RegisteredPlayerDto, PlayerRegistrationResponse>(registeredPlayer);

        return Created(string.Empty, playerRegistrationResponse);

    }

    [HttpPut("{playerId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeProfileAsync([FromRoute] long playerId, [FromBody] PlayerChangeProfileRequest request, [FromServices] IValidator<PlayerChangeProfileRequest> validator)
    {
        var requestValidationResult = validator.Validate(request);

        if (!requestValidationResult.IsValid)
            return BadRequest(ApiError.Instantiate(
                code: PlayersCodes.RequestValidation400,
                message: PlayersResource.RequestValidation400,
                requestValidationResult));

        var playerChangeProfileDto =
       mapper.Map<PlayerChangeProfileRequest, PlayerChangeProfileDto>(request);

        playerChangeProfileDto.Id = playerId;

        playerChangeProfileDto.UserId = authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken);

        await playerApplicationService.ChangeProfileAsync(playerChangeProfileDto);

        return NoContent();

    }

}

