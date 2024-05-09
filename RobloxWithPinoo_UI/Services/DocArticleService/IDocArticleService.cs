using RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos;
using RobloxWithPinoo_UI.Entity.Messages;

namespace RobloxWithPinoo_UI.Services.DocArticleService
{
    public interface IDocArticleService
    {
        Task<IEnumerable<DocArticles>> GetDocArticleByCategoryAsync(Guid categoryId, string token);
        Task<DocArticleDetails> ArticleDetails(Guid articleId, string token);
        Task<bool> CreateDocArticle(CreateDocArticle createDocArticle, string token);
        Task<IEnumerable<ListDocArticles>> GetAllListDocArticles(string token);
        Task<IEnumerable<ListDocArticles>> GetDocArticleListByCategoryAsync(Guid categoryId, string token);
        Task<bool> UpdateDocArticle(UpdateDocArticle updateDocArticle, Guid articleId, string token);
        Task<DocArticles> GetDocArticleByIdAsync(Guid articleId, string token);
        Task<bool> DeleteDocArticleAsync(Guid articleId, string token);
    }
}
