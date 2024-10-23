using Framework.Core.Domain.Exceptions;
using Framework.Core.Domain.ValueObjects;
using Games.Contract._Shared.Resources;

namespace Games.Domain.PlayerAggregate.Models;

public class PlayerName : ValueObject<PlayerName>
{
    public static PlayerName Instantiate(string firstName, string lastName) 
        => new(firstName, lastName);

    protected PlayerName() { }

    protected PlayerName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Validate();
    }

    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    public sealed override void Validate()
    {
        if (string.IsNullOrEmpty(FirstName.Trim()) || string.IsNullOrEmpty(LastName.Trim()))
            throw new BusinessException(Exceptions.PlayerHasInvalidName);
    }
}