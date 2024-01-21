using Framework.Core.Contracts;
using Framework.Test.Stubs;
using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;
using Players.SharedTestClasess.PlayerAggregate.Factories;
using Players.SharedTestClasess.Shared.Data;

namespace Players.SharedTestClasess.PlayerAggregate.Builders;

public class PlayerRegisterArgsTestBuilder
{
    private PlayerId _id;

    private string _firstName;

    private string _lastName;

    private DateOnly _birthDate;

    private Gender _gender;

    private string _userId;

    private IClock _clock;

    private IEventIdProvider _idProvider;

    private IDuplicateRegistrationCheckService _duplicateRegistrationCheckService;

    public static PlayerRegisterArgsTestBuilder Instantiate() => new();

    protected PlayerRegisterArgsTestBuilder()
    {
        _id = SomeBody.Id;

        _firstName = SomeBody.FirstName;

        _lastName = SomeBody.LastName;

        _birthDate = SomeBody.BirthDate;

        _gender = SomeBody.Gender;

        _userId = SomeBody.UserId;

        _clock = ClockStub.InstantiateOn(DateTimeTestData.CurrentDateTime);

        _idProvider = EventIdProviderStub.Instantiate();

        _duplicateRegistrationCheckService = PlayerDomainServicesTestFactory
            .CreateDuplicateRegistrationCheckServiceThatReturnsFalseResult();


    }

    public PlayerRegisterArgsTestBuilder WithId(PlayerId id)
    {
        _id = id;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithLastName(string lastName)
    {
        _lastName = lastName;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithBirthDate(DateOnly birthDate)
    {
        _birthDate = birthDate;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithGender(Gender gender)
    {
        _gender = gender;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithUserId(string userId)
    {
        _userId = userId;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithClock(IClock clock)
    {
        _clock = clock;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithIdProvider(IEventIdProvider idProvider)
    {
        _idProvider = idProvider;

        return this;
    }

    public PlayerRegisterArgsTestBuilder WithDuplicateRegistrationCheckService(IDuplicateRegistrationCheckService duplicateRegistrationCheckService)
    {
        _duplicateRegistrationCheckService = duplicateRegistrationCheckService;

        return this;
    }

    public PlayerRegisterArgs Build() => new()
    {
        Id = _id,

        FirstName = _firstName,

        LastName = _lastName,

        BirthDate = _birthDate,

        Gender = _gender,

        UserId = _userId,

        Clock = _clock,

        IdProvider = _idProvider,

        DuplicateRegistrationCheckService = _duplicateRegistrationCheckService
    };


}

