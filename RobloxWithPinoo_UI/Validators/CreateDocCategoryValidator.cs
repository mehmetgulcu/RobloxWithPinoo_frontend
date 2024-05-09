using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class CreateDocCategoryValidator : AbstractValidator<CreateDocCategory>
    {
        public CreateDocCategoryValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .NotNull()
                .WithName("Kategori adı");
        }
    }
}
