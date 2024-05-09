using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.DocArticleService;
using System;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class DocArticleController : Controller
    {
        private readonly IDocArticleService _docArticleService;

        public DocArticleController(IDocArticleService docArticleService)
        {
            _docArticleService = docArticleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid categoryId)
        {
            var token = HttpContext.Session.GetString("Token");

            var articles = await _docArticleService.GetDocArticleByCategoryAsync(categoryId, token);

            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid articleId)
        {
            var token = HttpContext.Session.GetString("Token");

            var articleDetail = await _docArticleService.ArticleDetails(articleId, token);

            return View(articleDetail);
        }
    }
}
