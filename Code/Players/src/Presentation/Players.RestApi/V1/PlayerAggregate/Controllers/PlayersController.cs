using FluentValidation;
using FluentValidation.Results;
using Framework.Core.Contracts;
using Framework.Presentation.AspNetCore.Contracts;
using Framework.Presentation.RestApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Players.ApplicationServices.PlayerAggregate.Dtos;
using Players.ApplicationServices.PlayerAggregate.Services;
using Players.Domain.PlayerAggregate.Exceptions;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;
using Players.RestApi.V1.PlayerAggregate.Responses.Register;

namespace Players.RestApi.V1.PlayerAggregate.Controllers;

[ApiController]
[Route("[Controller]")]
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
        try
        {
            ValidationResult requestValidationResult = validator.Validate(request);

            if (!requestValidationResult.IsValid)
                return BadRequest(ApiValidationError.Instantiate(requestValidationResult));

            PlayerRegistrationDto playerForRegister =
           _mapper.Map<PlayerRegistrationRequest, PlayerRegistrationDto>(request);

            //TODO:Replace this line after integrating with Identity Server
            playerForRegister.UserId = _authenticatedUser.GetSub() ?? "1";

            RegisteredPlayerDto registredPlayer = await _playerApplicationService.RegisterAsync(playerForRegister);

            PlayerRegisterationResponse playerRegisterationResponse =
                _mapper.Map<RegisteredPlayerDto, PlayerRegisterationResponse>(registredPlayer);

            return Created(string.Empty, playerRegisterationResponse);

        }
        catch (TheUserAlreadyRegistredException exception)
        {
            return Conflict(ApiError.Instantiate(exception));
        }


    }
}
