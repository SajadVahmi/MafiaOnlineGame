using Framework.Core.Domain.ValueObjects;
using Games.Domain.PlayerAggregate.Exceptions;

namespace Games.Domain.PlayerAggregate.Models;

public class PlayerUserId : ValueObject<PlayerUserId>
{
    public static PlayerUserId Instantiate(string value) => new(value);

    protected PlayerUserId() { }

    protected PlayerUserId(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new PlayerUserIdHasInvalidValueException();

        Value = value;
    }

    public string Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}