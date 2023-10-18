using Framework.Core.Domian.Exceptions;

namespace Players.Domain.PlayerAggregate.Exceptions;

public class TheUserAlreadyRegistredException : BusinessException
{
    public TheUserAlreadyRegistredException() :
        base(
           message: PlayerDomainExceptionMessages.TheUserAlreadyRegistred,
           code: PlayerDomainExceptionCodes.TheUserAlreadyRegistred)
    { }
}

