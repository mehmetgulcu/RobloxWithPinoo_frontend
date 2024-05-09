using RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;

namespace RobloxWithPinoo_UI.Services.DocCategoryService
{
    public interface IDocCategoryService
    {
        Task<List<ListDocCategories>> GetDocCategoriesForAllUsers(string token);
        Task<bool> CreateDocCategory(CreateDocCategory createDocCategory, string token);
        Task<bool> UpdateDocCategory(UpdateDocCategory updateDocCategory, Guid categoryId, string token);
        Task<DocCategory> GetDocCategoryByIdAsync(Guid categoryId, string token);
        Task<bool> DeleteDocCategoryAsync(Guid categoryId, string token);
    }
}
