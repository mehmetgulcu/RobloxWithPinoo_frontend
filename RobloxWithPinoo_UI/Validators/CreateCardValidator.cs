using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.CardDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class CreateCardValidator : AbstractValidator<CreateCardDto>
    {
        public CreateCardValidator()
        {
            RuleFor(model => model.CardName)
                .NotNull().WithMessage("Kart adı boş olamaz")
                .NotEmpty().WithMessage("Kart adı boş olamaz")
                .Must(BeValidCardName).WithMessage("Kart adı boşluk veya Türkçe karakter içeremez");
        }

        private bool BeValidCardName(string cardName)
        {
            if (string.IsNullOrWhiteSpace(cardName))
                return false;

            if (cardName.Contains(" "))
                return false;

            foreach (char c in cardName)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                    return false;
            }

            return true;
        }
    }
}
