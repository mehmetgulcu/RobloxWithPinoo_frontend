using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.ContactFormDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class SubmitContactFormValidator : AbstractValidator<SubmitContactFormDto>
    {
        public SubmitContactFormValidator()
        {
            RuleFor(form => form.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .WithName("İsim");

            RuleFor(form => form.Surname)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .WithName("Soyisim");

            RuleFor(form => form.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithName("Email");

            RuleFor(form => form.Message)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(500)
                .WithName("Mesaj");
        }
    }
}
