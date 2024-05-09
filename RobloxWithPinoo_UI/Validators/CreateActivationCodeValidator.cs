using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.ActivationCodeDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class CreateActivationCodeValidator : AbstractValidator<GenerateActivationCode>
    {
        public CreateActivationCodeValidator()
        {
            RuleFor(model => model.Amount)
                .NotEmpty().WithMessage("Kod adedi alanı boş olamaz")
                .NotNull().WithMessage("Kod adedi null olamaz")
                .InclusiveBetween(1, 100).WithMessage("Kod adedi alanı 1 ile 100 arasında olmalıdır");
        }
    }
}
