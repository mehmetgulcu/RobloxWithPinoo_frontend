using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.AuthDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(model => model.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithName("E-posta");

            RuleFor(model => model.Password)
                .NotNull()
                .NotEmpty()
                .WithName("Şifre");
        }
    }
}
