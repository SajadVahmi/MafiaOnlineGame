using Framework.Core.Domain.Exceptions;
using Games.Domain.Contracts.Resources;

namespace Games.Domain.PlayerAggregate.Exceptions;

public class PlayerNameHasInvalidValueException()
    : BusinessException(
        message: ExceptionMessages.PlayerHasInvalidName,
        code: ExceptionCodes.PlayerHasInvalidName,
        name: nameof(ExceptionMessages.PlayerHasInvalidName))
{
}