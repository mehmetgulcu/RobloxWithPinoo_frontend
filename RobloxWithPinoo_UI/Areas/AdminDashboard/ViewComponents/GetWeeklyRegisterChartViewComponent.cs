using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RobloxWithPinoo_UI.Services.AdminDashboardService;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.ViewComponents
{
    public class GetWeeklyRegisterChartViewComponent : ViewComponent
    {
        private readonly IAdminDashboardService _adminDashboardService;

        public GetWeeklyRegisterChartViewComponent(IAdminDashboardService adminDashboardService)
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

            var apiData = await _adminDashboardService.GetWeeklyRegisterChart(token);
            ViewBag.ChartData = JsonConvert.SerializeObject(apiData);

            return View();
        }
    }
}
