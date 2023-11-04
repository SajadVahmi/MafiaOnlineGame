using FluentValidation;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;

namespace Players.RestApi.V1.PlayerAggregate.Validations.Register;

public class PlayerRegistrationRequestValidator : AbstractValidator<PlayerRegistrationRequest>
{
    public PlayerRegistrationRequestValidator()
    {
        RuleFor(player => player.FirstName)
            .NotNull()
            .WithMessage(ValidationMessages.FirstNameIsRequired)
            .NotEmpty()
            .WithMessage(ValidationMessages.FirstNameIsRequired)
            .MaximumLength(3)
            .MaximumLength(50)
            .WithMessage(ValidationMessages.FirstNameLengthIsInvalid);

        RuleFor(player => player.LastName)
           .NotNull()
           .WithMessage(ValidationMessages.LastNameIsRequired)
           .NotEmpty()
           .WithMessage(ValidationMessages.LastNameIsRequired)
           .MaximumLength(3)
           .MaximumLength(50)
           .WithMessage(ValidationMessages.LastNameLengthIsInvalid);

        RuleFor(player => player.BirthDate)
          .NotNull()
          .WithMessage(ValidationMessages.BirthDateIsRequired)
          .NotEqual(default(DateOnly))
          .WithMessage(ValidationMessages.BirthDateIsInvalid);

        RuleFor(player => player.Gender)
         .NotNull()
         .WithMessage(ValidationMessages.GenderIsRequired)
         .IsInEnum()
         .WithMessage(ValidationMessages.GenderIsInvalid);
    }
}
