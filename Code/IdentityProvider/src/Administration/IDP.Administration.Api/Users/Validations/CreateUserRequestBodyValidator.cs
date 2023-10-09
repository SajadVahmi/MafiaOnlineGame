using FluentValidation;
using IDP.Administration.Api.Users.Models;

namespace IDP.Administration.Api.Users.Validations
{
    public class CreateUserRequestBodyValidator : AbstractValidator<CreateUserRequestBody>
    {
        public CreateUserRequestBodyValidator()
        {
            RuleFor(body => body.Email)
                .EmailAddress();

            RuleFor(body => body.Mobile)
                .NotNull()
                .WithMessage("Mobile number is required.")
                .NotEmpty()
                .WithMessage("Mobile number is required.")
                .MinimumLength(11)
                .WithMessage("Mobile number must be 11 digits.")
                .MaximumLength(11)
                .WithMessage("Mobile number must be 11 digits.");


            

        }
    }
}
