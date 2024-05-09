using FluentValidation;
using RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos;

namespace RobloxWithPinoo_UI.Validators
{
    public class CreateDocArticleValidator : AbstractValidator<CreateDocArticle>
    {
        public CreateDocArticleValidator()
        {
            RuleFor(model => model.Title)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .WithName("Makale başlığı");

            RuleFor(model => model.Content)
                .NotEmpty()
                .NotNull()
                .WithName("Makale içeriği");

            RuleFor(model => model.ImageUrl)
                .NotEmpty()
                .NotNull()
                .WithName("Makale görsel yolu");

            RuleFor(model => model.DocCategoryId)
                .NotEmpty()
                .NotNull()
                .WithName("Kategori");
        }
    }
}
