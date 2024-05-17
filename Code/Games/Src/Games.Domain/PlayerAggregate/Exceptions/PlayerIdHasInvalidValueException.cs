using Framework.Core.Domain.Exceptions;
using Games.Domain.Contracts.Resources;

namespace Games.Domain.PlayerAggregate.Exceptions;

public class PlayerIdHasInvalidValueException() 
    :BusinessException(
        message:ExceptionMessages.PlayerHasInvalidId,
        code:ExceptionCodes.PlayerHasInvalidId,
        name:nameof(ExceptionMessages.PlayerHasInvalidId))
{
}