using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.AccountService;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.ViewComponents
{
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class UserAccountInfoViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;

        public UserAccountInfoViewComponent(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = HttpContext.Session.GetString("Token");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (string.IsNullOrEmpty(token) || (jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value != "User"))
            {
                return Content("Kullanıcı bulunamadı");
            }

            var userInfo = await _accountService.GetAccountInfoAsync(token);

            return View(userInfo);
        }
    }
}
