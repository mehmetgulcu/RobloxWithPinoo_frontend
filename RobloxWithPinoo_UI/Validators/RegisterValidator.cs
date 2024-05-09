using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.AuthDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .WithName("İsim");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .WithName("Soyisim");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithName("E-posta");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .WithName("Şifre");
        }
    }
}
