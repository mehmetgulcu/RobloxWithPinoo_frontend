using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Services.AdminDashboardService;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.ViewComponents
{
    public class GetTotalActiveCardsCountViewComponent : ViewComponent
    {
        private readonly IAdminDashboardService _adminDashboardService;

        public GetTotalActiveCardsCountViewComponent(IAdminDashboardService adminDashboardService)
        {
            _adminDashboardService = adminDashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (string.IsNullOrEmpty(token) || (jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value != "Admin"))
            {
                return Content("Kullanıcı bulunamadı");
            }

            var count = await _adminDashboardService.GetTotalActiveCardsCount(token);

            return View(count);
        }
    }
}
