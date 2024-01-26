using FluentValidation;
using IDP.Administration.Api.V1.Users.Models;

namespace IDP.Administration.Api.V1.Users.Validations;

public class CreateUserRequestBodyValidator:AbstractValidator<CreateUserRequestBody>
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

        RuleFor(body => body.LockoutEnabled)
            .NotNull()
            .WithMessage("LockoutEnabled is required.");

        RuleFor(body => body.TwoFactorEnabled)
            .NotNull()
            .WithMessage("TwoFactorEnabled is required.");

        RuleFor(body => body.OtpSmsEnabled)
            .NotNull()
            .WithMessage("OtpSmsEnabled is required.");

    }
}