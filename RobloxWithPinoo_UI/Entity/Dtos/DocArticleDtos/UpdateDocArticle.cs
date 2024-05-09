using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;

namespace RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos
{
    public class UpdateDocArticle
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public Guid DocCategoryId { get; set; }
        public IList<ListDocCategories> Categories { get; set; }
    }
}
