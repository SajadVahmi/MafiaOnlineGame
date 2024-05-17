using Framework.Core.Domain.Exceptions;
using Games.Domain.Contracts.Resources;

namespace Games.Domain.PlayerAggregate.Exceptions;

public class PlayerUserIdHasInvalidValueException()
    : BusinessException(
        message: ExceptionMessages.PlayerHasInvalidUserId,
        code: ExceptionCodes.PlayerHasInvalidUserId,
        name: nameof(ExceptionMessages.PlayerHasInvalidUserId))
{
}