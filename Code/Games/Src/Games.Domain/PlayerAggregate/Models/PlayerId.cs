using System.ComponentModel.DataAnnotations;
using Framework.Core.Domain.ValueObjects;
using Games.Domain.PlayerAggregate.Exceptions;

namespace Games.Domain.PlayerAggregate.Models;

public class PlayerId : ValueObject<PlayerId>
{
    public static PlayerId Instantiate(string value) => new(value);
    public static PlayerId Instantiate(Guid value) => new(value);

    protected PlayerId() { }

    protected PlayerId(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new PlayerIdHasInvalidValueException();

        Value = value;
    }

    protected PlayerId(Guid value)
    {
        if (value==default)
            throw new PlayerIdHasInvalidValueException();

        Value = value.ToString();
    }


    public string Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}