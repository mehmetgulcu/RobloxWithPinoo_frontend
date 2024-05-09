using RobloxWithPinoo_UI.Entity.Dtos.AdminDashboardDtos;

namespace RobloxWithPinoo_UI.Services.AdminDashboardService
{
    public interface IAdminDashboardService
    {
        Task<TotalDocArticlesCount> GetTotalDocArticlesCount(string token);
        Task<TotalDocCategoriesCount> GetTotalDocCategoriesCount(string token);
        Task<List<NumberOfArticlesPerCategory>> GetNumberOfArticlesPerCategory(string token);
        Task<TotalActiveCardsCount> GetTotalActiveCardsCount(string token);
        Task<TotalUsersCount> GetTotalUsersCount(string token);
        Task<DailyRegisterChart> GetDailyRegisterChart(string token);
        Task<List<WeeklyRegisterChart>> GetWeeklyRegisterChart(string token);
        Task<List<MonthlyRegisterChart>> GetMonthlyRegisterChart(string token);
        Task<List<YearlyRegisterChart>> GetYearlyRegisterChart(string token);
        Task<GeneratedActivationCodesCount> GetGeneratedActivationCodesCount(string token);
        Task<TotalActivatedCodesCount> GetTotalActivatedCodesCount(string token);
        Task<TotalNotActivatedCodesCount> GetTotalNotActivatedCodesCount(string token);
    }
}
