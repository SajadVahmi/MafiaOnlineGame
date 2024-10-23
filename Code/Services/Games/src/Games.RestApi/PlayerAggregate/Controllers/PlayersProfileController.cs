using Framework.Core.Application.Commands;
using Framework.Core.Domain.Queries;
using Framework.Core.ServiceContracts;
using Framework.Presentation.RestApi.Responses;
using Games.Application.PlayerAggregate.Commands.ChangePlayerGender;
using Games.Application.PlayerAggregate.Commands.RegisterPlayer;
using Games.Application.PlayerAggregate.Commands.RenamePlayer;
using Games.Contract.PlayerAggregate.Dto;
using Games.Query.PlayerAggregate.Queries.ViewProfile;
using Games.RestApi.PlayerAggregate.Requests;
using Games.RestApi.PlayerAggregate.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Games.RestApi.PlayerAggregate.Controllers;

[ApiController]
[Authorize]
[Route("api/player-profiles")]
public class PlayersProfileController(
    ICommandBus commandBus,
    IQueryBus queryBus,
    IMapperAdapter mapper) : ControllerBase
{

    [HttpGet("me")]
    [ProducesResponseType(type: typeof(ViewProfileResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> ViewProfileAsync(CancellationToken cancellationToken = default)
    {
        var profile = await queryBus.ExecuteAsync<ViewProfileQuery, ViewProfileQueryResult?>(new ViewProfileQuery(), cancellationToken);
        if (profile == null)
            return NotFound();
        var viewProfileResponse = mapper.Map<ViewProfileResponse>(profile);
        return Ok(viewProfileResponse);
    }


    [HttpPost]
    [ProducesResponseType(type: typeof(RegisteredPlayerResponse), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ApiError), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterPlayerRequest request, CancellationToken cancellationToken = default)
    {
        var command = mapper.Map<RegisterPlayerCommand>(request);
        var registeredPlayer = await commandBus.SendAsync<RegisterPlayerCommand, RegisteredPlayerDto>(command, cancellationToken);
        var registeredPlayerResponse = mapper.Map<RegisteredPlayerResponse>(registeredPlayer);
        return CreatedAtAction(nameof(ViewProfileAsync), registeredPlayerResponse);
    }

    [HttpPut("{playerId}/name")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RenameAsync([FromRoute] string playerId, [FromBody] RenamePlayerRequest request, CancellationToken cancellationToken = default)
    {
        var command = mapper.Map<RenamePlayerCommand>(request);
        command.PlayerId = playerId;
        await commandBus.SendAsync(command, cancellationToken);
        return NoContent();
    }


    [HttpPut("{playerId}/gender")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeGenderAsync([FromRoute] string playerId, [FromBody] ChangePlayerGenderRequest request, CancellationToken cancellationToken = default)
    {
        var command = mapper.Map<ChangePlayerGenderCommand>(request);
        command.PlayerId = playerId;
        await commandBus.SendAsync(command, cancellationToken);
        return Ok();
    }
}