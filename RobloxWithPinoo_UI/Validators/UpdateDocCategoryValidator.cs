using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class UpdateDocCategoryValidator : AbstractValidator<UpdateDocCategory>
    {
        public UpdateDocCategoryValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .NotNull()
                .WithName("Kategori adı");
        }
    }
}
