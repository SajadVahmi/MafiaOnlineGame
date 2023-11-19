using Players.Contracts.Enums;
using Players.RestApi.IntegrationTests.V1.PlayerAggregates.TestData;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.TestBuilders;

public class PlayerRegistrationRequestBuilder
{
    private string? _firstName;

    private string? _lastName;

    private DateOnly? _birthDate;

    private Gender? _gender;

    public static PlayerRegistrationRequestBuilder Instantiate()=>new();

    protected PlayerRegistrationRequestBuilder()
    {
        _firstName = PlayerTestData.Somebody.FirstName;

        _lastName= PlayerTestData.Somebody.LastName;

        _birthDate=PlayerTestData.Somebody.BirthDate;

        _gender=PlayerTestData.Somebody.Gender;

    }

    public PlayerRegistrationRequestBuilder WithFirstName(string? firstName)
    {
        _firstName = firstName;

        return this;
    }
    public PlayerRegistrationRequestBuilder WithLastName(string? lastName)
    {
        _lastName = lastName;

        return this;
    }

    public PlayerRegistrationRequestBuilder WithBirthDate(DateOnly? birthDate)
    {
        _birthDate = birthDate;

        return this;
    }

    public PlayerRegistrationRequestBuilder WithGender(Gender? gender)
    {
        _gender = gender;

        return this;
    }

    public PlayerRegistrationRequest Build() =>
        new()
        {
            FirstName = _firstName,
            LastName = _lastName,
            BirthDate = _birthDate,
            Gender = _gender
        };

}
