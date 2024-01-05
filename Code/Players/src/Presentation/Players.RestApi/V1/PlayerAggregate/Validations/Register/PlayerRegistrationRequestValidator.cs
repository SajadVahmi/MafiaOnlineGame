using FluentValidation;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;

namespace Players.RestApi.V1.PlayerAggregate.Validations.Register;

public class PlayerRegistrationRequestValidator : AbstractValidator<PlayerRegistrationRequest>
{
    public PlayerRegistrationRequestValidator()
    {
        
        RuleFor(player => player.FirstName)
            .NotNull()
            .WithMessage(PlayerRegistrationValidationMessages.FirstNameIsRequired)
            .MinimumLength(3)
            .WithMessage(PlayerRegistrationValidationMessages.FirstNameLengthIsInvalid)
            .MaximumLength(50)
            .WithMessage(PlayerRegistrationValidationMessages.FirstNameLengthIsInvalid);

        RuleFor(player => player.LastName)
           .NotNull()
           .WithMessage(PlayerRegistrationValidationMessages.LastNameIsRequired)
           .MinimumLength(3)
           .WithMessage(PlayerRegistrationValidationMessages.LastNameLengthIsInvalid)
           .MaximumLength(50)
           .WithMessage(PlayerRegistrationValidationMessages.LastNameLengthIsInvalid);

        RuleFor(player => player.BirthDate)
          .Cascade(CascadeMode.Stop)
          .NotNull()
          .WithMessage(PlayerRegistrationValidationMessages.BirthDateIsRequired)
          .NotEqual(default(DateOnly))
          .WithMessage(PlayerRegistrationValidationMessages.BirthDateIsInvalid);

        RuleFor(player => player.Gender)
         .Cascade(CascadeMode.Stop)
         .NotNull()
         .WithMessage(PlayerRegistrationValidationMessages.GenderIsRequired)
         .IsInEnum()
         .WithMessage(PlayerRegistrationValidationMessages.GenderIsInvalid);
    }
}
