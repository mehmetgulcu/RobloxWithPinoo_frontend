using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;

namespace RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos
{
    public class ListDocArticles
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public IList<ListDocCategories> Categories { get; set; }
        public string DocCategoryName { get; set; }
        public string CreatedDate { get; set; }
        public int IndexNo { get; set; }
    }
}
