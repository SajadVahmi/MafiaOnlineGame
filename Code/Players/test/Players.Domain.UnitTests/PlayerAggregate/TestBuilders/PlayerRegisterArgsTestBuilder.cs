using Framework.Core.Contracts;
using Framework.Test.Stubs;
using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;
using Players.Domain.UnitTests.PlayerAggregate.Factories;
using Players.Domain.UnitTests.PlayerAggregate.TestData;
using IClock = Framework.Core.Contracts.IClock;

namespace Players.Domain.UnitTests.PlayerAggregate.TestBuilders;

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
        _id = PlayerTestData.RandomPlayer.Id;

        _firstName = PlayerTestData.RandomPlayer.FirstName;

        _lastName = PlayerTestData.RandomPlayer.LastName;

        _birthDate = PlayerTestData.RandomPlayer.BirthDate;

        _gender = PlayerTestData.RandomPlayer.Gender;

        _userId = PlayerTestData.RandomPlayer.UserId;

        _clock = ClockStub.Instantiate();

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
