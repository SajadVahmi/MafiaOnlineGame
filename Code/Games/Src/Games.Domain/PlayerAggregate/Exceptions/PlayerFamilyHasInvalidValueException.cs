using Framework.Core.Domain.Exceptions;
using Games.Domain.Contracts.Resources;

namespace Games.Domain.PlayerAggregate.Exceptions;

public class PlayerFamilyHasInvalidValueException()
    : BusinessException(
        message: ExceptionMessages.PlayerHasInvalidFamily,
        code: ExceptionCodes.PlayerHasInvalidFamily,
        name: nameof(ExceptionMessages.PlayerHasInvalidFamily))
{
}