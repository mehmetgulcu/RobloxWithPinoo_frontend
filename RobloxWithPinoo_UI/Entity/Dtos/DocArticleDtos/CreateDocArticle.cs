using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using System.ComponentModel.DataAnnotations;

namespace RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos
{
    public class CreateDocArticle
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public Guid DocCategoryId { get; set; }
        public IList<ListDocCategories> Categories { get; set; }
    }
}
