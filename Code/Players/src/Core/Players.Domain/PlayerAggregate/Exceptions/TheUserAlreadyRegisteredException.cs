using Framework.Core.Domain.Exceptions;
using Players.Contracts.Resources;

namespace Players.Domain.PlayerAggregate.Exceptions;

public class TheUserAlreadyRegisteredException() : BusinessException(
    message: PlayersResource.Player100TheUserAlreadyRegistred,
    code: PlayersCodes.Player100TheUserAlreadyRegistred,
    name: nameof(PlayersResource.Player100TheUserAlreadyRegistred));
