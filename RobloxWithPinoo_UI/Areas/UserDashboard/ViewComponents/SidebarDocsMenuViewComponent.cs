using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.DocCategoryService;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.ViewComponents
{
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class SidebarDocsMenuViewComponent : ViewComponent
    {
        private readonly IDocCategoryService _categoryService;

        public SidebarDocsMenuViewComponent(IDocCategoryService categoryService)
        {
            _categoryService = categoryService;
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

            var categories = await _categoryService.GetDocCategoriesForAllUsers(token);

            return View(categories);
        }
    }
}
