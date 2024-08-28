using Framework.Core.Application.Commands;
using Games.Application.PlayerAggregate.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Games.RestApi.PlayerAggregate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    public PlayersController()
    {
                
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

}