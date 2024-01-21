using FluentValidation;
using Players.Contracts.Resources;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;

namespace Players.RestApi.V1.PlayerAggregate.Validations.Register;

public class PlayerRegistrationRequestValidator : AbstractValidator<PlayerRegistrationRequest>
{
    public PlayerRegistrationRequestValidator()
    {
        
        RuleFor(player => player.FirstName)
            .NotNull()
            .WithMessage(PlayersResource.Player104FirstNameIsRequired)
            .MinimumLength(3)
            .WithMessage(PlayersResource.Player105FirstNameLengthIsInvalid)
            .MaximumLength(50)
            .WithMessage(PlayersResource.Player105FirstNameLengthIsInvalid);

        RuleFor(player => player.LastName)
           .NotNull()
           .WithMessage(PlayersResource.Player108LastNameIsRequired)
           .MinimumLength(3)
           .WithMessage(PlayersResource.Player109LastNameLengthIsInvalid)
           .MaximumLength(50)
           .WithMessage(PlayersResource.Player109LastNameLengthIsInvalid);

        RuleFor(player => player.BirthDate)
          .Cascade(CascadeMode.Stop)
          .NotNull()
          .WithMessage(PlayersResource.Player103BirthDateIsRequired)
          .NotEqual(default(DateOnly))
          .WithMessage(PlayersResource.Player102BirthDateIsInvalid);

        RuleFor(player => player.Gender)
         .Cascade(CascadeMode.Stop)
         .NotNull()
         .WithMessage(PlayersResource.Player107GenderIsRequired)
         .IsInEnum()
         .WithMessage(PlayersResource.Player106GenderIsInvalid);
    }
}
