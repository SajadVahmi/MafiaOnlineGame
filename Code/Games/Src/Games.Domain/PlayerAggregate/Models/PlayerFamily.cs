using Framework.Core.Domain.ValueObjects;
using Games.Domain.Contracts.Constants;
using Games.Domain.PlayerAggregate.Exceptions;

namespace Games.Domain.PlayerAggregate.Models;

public class PlayerFamily : ValueObject<PlayerFamily>
{
    public static PlayerFamily Instantiate(string value) => new(value);

    protected PlayerFamily() { }

    protected PlayerFamily(string value)
    {
        if (string.IsNullOrEmpty(value) ||
            value.Length > PlayerConstants.PlayerFamilyValidLength)
            throw new PlayerFamilyHasInvalidValueException();

        Value = value;
    }

    public string Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}