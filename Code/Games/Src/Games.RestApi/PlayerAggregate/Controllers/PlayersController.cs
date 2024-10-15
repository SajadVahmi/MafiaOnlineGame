using Framework.Core.Application.Commands;
using Framework.Core.Application.Queries;
using Games.Application.PlayerAggregate.Commands.ChangePlayerGender;
using Games.Application.PlayerAggregate.Commands.RegisterPlayer;
using Games.Application.PlayerAggregate.Commands.RenamePlayer;
using Games.Query.PlayerAggregate.Queries.ViewProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Games.RestApi.PlayerAggregate.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    [HttpGet("profile")]
    public async Task<IActionResult> ViewProfileAsync(
        [FromServices] IQueryBus queryBus,
        CancellationToken cancellationToken = default)
    {
        var profile =await queryBus.ExecuteAsync<ViewProfileQuery, ViewProfileQueryResult?>(new ViewProfileQuery(), cancellationToken);
        if (profile == null)
            return NotFound();
        return Ok(profile);
    }


    [HttpPost]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterPlayerCommand command,
        [FromServices] ICommandBus commandBus,
        CancellationToken cancellationToken = default)
    {
        await commandBus.SendAsync(command, cancellationToken);
        return Ok();
    }

    [HttpPost("rename")]
    public async Task<IActionResult> RenameAsync(
        [FromBody] RenamePlayerCommand command,
        [FromServices] ICommandBus commandBus,
        CancellationToken cancellationToken = default)
    {
        await commandBus.SendAsync(command, cancellationToken);
        return Ok();
    }


    [HttpPost("gender")]
    public async Task<IActionResult> RenameAsync(
        [FromBody] ChangePlayerGenderCommand command,
        [FromServices] ICommandBus commandBus,
        CancellationToken cancellationToken = default)
    {
        await commandBus.SendAsync(command, cancellationToken);
        return Ok();
    }
}