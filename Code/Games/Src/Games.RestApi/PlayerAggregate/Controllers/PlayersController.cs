using Framework.Core.Application.Commands;
using Games.Application.PlayerAggregate.Commands.RegisterPlayer;
using Games.Application.PlayerAggregate.Commands.RenamePlayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Games.RestApi.PlayerAggregate.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterPlayerCommand command,
        [FromServices] ICommandBus commandBus,
        CancellationToken cancellationToken = default)
    {
        var user = User.Claims;
        await commandBus.SendAsync(command, cancellationToken);
        return Ok();
    }

    [HttpPost("Rename")]
    public async Task<IActionResult> RenameAsync(
        [FromBody] RenamePlayerCommand command,
        [FromServices] ICommandBus commandBus,
        CancellationToken cancellationToken = default)
    {
        var user = User.Claims;
        await commandBus.SendAsync(command, cancellationToken);
        return Ok();
    }

}