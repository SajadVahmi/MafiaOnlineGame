using Framework.Core.Domian.Exceptions;
using Players.Contracts.Resources;

namespace Players.Domain.PlayerAggregate.Exceptions;

public class TheUserAlreadyRegistredException : BusinessException
{
    public TheUserAlreadyRegistredException() :
        base(
           message: PlayersResource.Player100TheUserAlreadyRegistred,
           code: PlayersCodes.Player100TheUserAlreadyRegistred,
           name:nameof(PlayersResource.Player100TheUserAlreadyRegistred))
    { }
}
