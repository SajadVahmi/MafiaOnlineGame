using Framework.Core.Domain.ValueObjects;
using Games.Domain.Contracts.Constants;
using Games.Domain.PlayerAggregate.Exceptions;

namespace Games.Domain.PlayerAggregate.Models;

public class PlayerName : ValueObject<PlayerName>
{
    public static PlayerName Instantiate(string value) => new(value);

    protected PlayerName() { }

    protected PlayerName(string value)
    {
        if (string.IsNullOrEmpty(value)||
            value.Length>PlayerConstants.PlayerNameValidLength)
            throw new PlayerNameHasInvalidValueException();

        Value = value;
    }

    public string Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}