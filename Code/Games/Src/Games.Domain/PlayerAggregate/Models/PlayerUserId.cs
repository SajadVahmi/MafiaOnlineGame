using Framework.Core.Domain.Exceptions;
using Framework.Core.Domain.ValueObjects;
using Games.Domain.Contracts.Resources;

namespace Games.Domain.PlayerAggregate.Models;

public class PlayerUserId : ValueObject<PlayerUserId>
{
    public static PlayerUserId Instantiate(string value) => new(value);

    protected PlayerUserId() { }

    protected PlayerUserId(string value)
    {
        Value = value;
        Validate();
    }
    public string Value { get;} = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public sealed override void Validate()
    {
        //if (string.IsNullOrEmpty(Value))
           //// throw new BusinessException(Exceptions.PlayerHasInvalidUserId);
    }
}