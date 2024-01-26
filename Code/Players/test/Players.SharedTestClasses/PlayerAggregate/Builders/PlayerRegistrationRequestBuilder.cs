using Players.Contracts.Enums;
using Players.RestApi.V1.PlayerAggregate.Requests.ChangeProfile;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;

namespace Players.SharedTestClasses.PlayerAggregate.Builders;

public class PlayerRegistrationRequestBuilder
{
    private string? _firstName;

    private string? _lastName;

    private DateOnly? _birthDate;

    private Gender? _gender;

    public static PlayerRegistrationRequestBuilder Instantiate() => new();

    protected PlayerRegistrationRequestBuilder()
    {
        _firstName = SomeBody.FirstName;

        _lastName = SomeBody.LastName;

        _birthDate = SomeBody.BirthDate;

        _gender = SomeBody.Gender;

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

public class PlayerChangeProfileRequestBuilder
{
    private string? _firstName;

    private string? _lastName;

    private DateOnly? _birthDate;

    private Gender? _gender;

    public static PlayerChangeProfileRequestBuilder Instantiate() => new();

    protected PlayerChangeProfileRequestBuilder()
    {
        _firstName = SomeBody.FirstName;

        _lastName = SomeBody.LastName;

        _birthDate = SomeBody.BirthDate;

        _gender = SomeBody.Gender;

    }

    public PlayerChangeProfileRequestBuilder WithFirstName(string? firstName)
    {
        _firstName = firstName;

        return this;
    }
    public PlayerChangeProfileRequestBuilder WithLastName(string? lastName)
    {
        _lastName = lastName;

        return this;
    }

    public PlayerChangeProfileRequestBuilder WithBirthDate(DateOnly? birthDate)
    {
        _birthDate = birthDate;

        return this;
    }

    public PlayerChangeProfileRequestBuilder WithGender(Gender? gender)
    {
        _gender = gender;

        return this;
    }

    public PlayerChangeProfileRequest Build() =>
        new()
        {
            FirstName = _firstName,

            LastName = _lastName,

            BirthDate = _birthDate,

            Gender = _gender
        };

}
