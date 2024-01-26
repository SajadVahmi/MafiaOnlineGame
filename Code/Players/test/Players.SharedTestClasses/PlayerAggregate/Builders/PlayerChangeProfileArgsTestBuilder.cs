using Framework.Core.Contracts;
using Framework.Test.Stubs;
using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Models;
using Players.SharedTestClasses.Shared.Data;

namespace Players.SharedTestClasses.PlayerAggregate.Builders;

public class PlayerChangeProfileArgsTestBuilder
{
    private string _firstName;

    private string _lastName;

    private DateOnly _birthDate;

    private Gender _gender;

    private IClock _clock;

    private IEventIdProvider _idProvider;

    public static PlayerChangeProfileArgsTestBuilder Instantiate() => new();

    protected PlayerChangeProfileArgsTestBuilder()
    {
        _firstName = SomeBody.FirstName;

        _lastName = SomeBody.LastName;

        _birthDate = SomeBody.BirthDate;

        _gender = SomeBody.Gender;

        _clock = ClockStub.InstantiateOn(DateTimeTestData.CurrentDateTime);

        _idProvider = EventIdProviderStub.Instantiate();
    }

    public PlayerChangeProfileArgsTestBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;

        return this;
    }
    public PlayerChangeProfileArgsTestBuilder WithLastName(string lastName)
    {
        _lastName = lastName;

        return this;
    }

    public PlayerChangeProfileArgsTestBuilder WithBirthDate(DateOnly birthDate)
    {
        _birthDate = birthDate;

        return this;
    }

    public PlayerChangeProfileArgsTestBuilder WithGender(Gender gender)
    {
        _gender = gender;

        return this;
    }
    public PlayerChangeProfileArgsTestBuilder WithClock(IClock clock)
    {
        _clock = clock;

        return this;
    }

    public PlayerChangeProfileArgsTestBuilder WithIdProvider(IEventIdProvider idProvider)
    {
        _idProvider = idProvider;

        return this;
    }
    public PlayerChangeProfileArgs Build() =>
        new()
        {
            FirstName = _firstName,

            LastName = _lastName,

            BirthDate = _birthDate,

            Gender = _gender,

            Clock = _clock,

            IdProvider = _idProvider
        };

}
