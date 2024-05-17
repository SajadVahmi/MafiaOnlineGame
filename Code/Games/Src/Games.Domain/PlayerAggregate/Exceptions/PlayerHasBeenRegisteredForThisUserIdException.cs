using Framework.Core.Domain.Exceptions;
using Games.Domain.Contracts.Resources;

namespace Games.Domain.PlayerAggregate.Exceptions;

public class PlayerHasBeenRegisteredForThisUserIdException()
    : BusinessException(
        message: ExceptionMessages.PlayerHasBeenRegisteredForThisUserId,
        code: ExceptionCodes.PlayerHasBeenRegisteredForThisUserId,
        name: nameof(ExceptionMessages.PlayerHasBeenRegisteredForThisUserId))
{
}