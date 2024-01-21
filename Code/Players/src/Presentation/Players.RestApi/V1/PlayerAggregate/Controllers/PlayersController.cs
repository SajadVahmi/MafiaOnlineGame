using FluentValidation;
using FluentValidation.Results;
using Framework.Core.ApplicationServices.Exceptions;
using Framework.Core.Contracts;
using Framework.Presentation.RestApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Players.ApplicationServices.PlayerAggregate.Dtos;
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
public class PlayersController : ControllerBase
{
    private readonly IPlayerApplicationService _playerApplicationService;

    private readonly IMapperAdapter _mapper;

    private readonly IAuthenticatedUser _authenticatedUser;

    public PlayersController(
        IPlayerApplicationService playerApplicationService,
        IMapperAdapter mapper,
        IAuthenticatedUser authenticatedUser)
    {
        _playerApplicationService = playerApplicationService;

        _mapper = mapper;

        _authenticatedUser = authenticatedUser;
    }

    [HttpPost]
    [ProducesResponseType(type: typeof(PlayerRegisterationResponse), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status409Conflict)]
    [ProducesResponseType(type: typeof(IEnumerable<ApiValidationError>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] PlayerRegistrationRequest request, [FromServices] IValidator<PlayerRegistrationRequest> validator)
    {

        ValidationResult requestValidationResult = validator.Validate(request);

        if (!requestValidationResult.IsValid)
            return BadRequest(ApiValidationError.Instantiate(requestValidationResult));

        PlayerRegistrationDto playerForRegister =
       _mapper.Map<PlayerRegistrationRequest, PlayerRegistrationDto>(request);

        playerForRegister.UserId = _authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken);

        RegisteredPlayerDto registredPlayer;

        try
        {
            registredPlayer = await _playerApplicationService.RegisterAsync(playerForRegister);
        }
        catch (TheUserAlreadyRegistredException exception)
        {
            return Conflict(ApiError.Instantiate(exception));
        }

        PlayerRegisterationResponse playerRegisterationResponse =
            _mapper.Map<RegisteredPlayerDto, PlayerRegisterationResponse>(registredPlayer);

        return Created(string.Empty, playerRegisterationResponse);

    }

    [HttpPut("{playerId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(type: typeof(IEnumerable<ApiValidationError>), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeProfileAsync([FromRoute] long playerId,[FromBody] PlayerChangeProfileRequest request, [FromServices] IValidator<PlayerChangeProfileRequest> validator)
    {
        ValidationResult requestValidationResult = validator.Validate(request);

        if (!requestValidationResult.IsValid)
            return BadRequest(ApiValidationError.Instantiate(requestValidationResult));

        PlayerChangeProfileDto playerChangeProfileDto =
       _mapper.Map<PlayerChangeProfileRequest, PlayerChangeProfileDto>(request);

        playerChangeProfileDto.Id = playerId;
        
        playerChangeProfileDto.UserId = _authenticatedUser.GetSub() ?? throw new Exception(Exceptions.CannotOptainUserIdFromAccessToken);

        try
        {
            await _playerApplicationService.ChangeProfileAsync(playerChangeProfileDto);
        }
        catch (AggregateNotFoundException exception)
        {
            return NotFound(ApiError.Instantiate(exception));
        }

        return NoContent();

    }

}

