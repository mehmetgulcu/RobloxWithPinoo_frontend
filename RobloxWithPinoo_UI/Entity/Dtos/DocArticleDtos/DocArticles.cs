﻿namespace RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos
{
    public class DocArticles
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public Guid DocCategoryId { get; set; }
        public string DocCategoryName { get; set; }
        public int IndexNo { get; set; }
    }
}
