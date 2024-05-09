using AspNetCoreHero.ToastNotification.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.DocArticleService;
using RobloxWithPinoo_UI.Services.DocCategoryService;
using RobloxWithPinoo_UI.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [TypeFilter(typeof(CheckAdminTokenFilter))]
    public class DocArticleController : Controller
    {
        private readonly IDocArticleService _articleService;
        private readonly IDocCategoryService _categoryService;
        private readonly INotyfService _notyf;

        public DocArticleController(IDocArticleService articleService, IDocCategoryService categoryService, INotyfService notyf)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");

            ViewBag.TOKEN = token;
            ViewBag.BASEURL = Constants.BaseUrl.BackendBaseUrl;

            var listDocArticles = await _articleService.GetAllListDocArticles(token);

            return View(listDocArticles);
        }

        [HttpGet]
        public async Task<IActionResult> DocArticleByCategory(Guid categoryId)
        {
            var token = HttpContext.Session.GetString("Token");

            ViewBag.TOKEN = token;
            ViewBag.BASEURL = Constants.BaseUrl.BackendBaseUrl;

            var articles = await _articleService.GetDocArticleListByCategoryAsync(categoryId, token);

            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> CreateDocArticle()
        {
            var token = HttpContext.Session.GetString("Token");

            var categoriesFromService = await _categoryService.GetDocCategoriesForAllUsers(token);

            var categoriesDto = categoriesFromService.Select(category => new ListDocCategories
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return View(new CreateDocArticle { Categories = categoriesDto });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocArticle(CreateDocArticle createDocArticle)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");

                var validator = new CreateDocArticleValidator();
                var validationResult = validator.Validate(createDocArticle);

                if (validationResult.IsValid)
                {
                    var result = await _articleService.CreateDocArticle(createDocArticle, token);

                    _notyf.Success("Makale Başarılı Şekilde Oluşturuldu.");
                    return RedirectToAction("Index", "DocArticle", new { area = "AdminDashboard" });
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                    var categoriesFromService = await _categoryService.GetDocCategoriesForAllUsers(token);

                    var categoriesDto = categoriesFromService.Select(category => new ListDocCategories
                    {
                        Id = category.Id,
                        Name = category.Name
                    }).ToList();

                    return View(new CreateDocArticle { Categories = categoriesDto });
                }

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Index", "DocArticle", new { area = "AdminDashboard" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDocArticle(Guid articleId)
        {
            var token = HttpContext.Session.GetString("Token");

            var article = await _articleService.GetDocArticleByIdAsync(articleId, token);

            if (article == null)
            {
                return NotFound("Makale bulunamadı.");
            }

            var categoriesFromService = await _categoryService.GetDocCategoriesForAllUsers(token);

            var categoriesDto = categoriesFromService.Select(category => new ListDocCategories
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            var articleUpdateDto = new UpdateDocArticle
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Categories = categoriesDto,
            };

            ViewBag.TOKEN = token;
            ViewBag.ArticleId = articleId;
            ViewBag.CategoryId = article.DocCategoryId;

            return View(articleUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDocArticle(UpdateDocArticle updateDocArticle)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");

                var validator = new UpdateDocArticleValidator();
                var validationResult = validator.Validate(updateDocArticle);

                if (validationResult.IsValid)
                {
                    var result = await _articleService.UpdateDocArticle(updateDocArticle, updateDocArticle.Id, token);

                    _notyf.Success("Makale Başarılı Şekilde Güncellendi.");
                    return RedirectToAction("Index", "DocArticle", new { area = "AdminDashboard" });
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                    var categoriesFromService = await _categoryService.GetDocCategoriesForAllUsers(token);

                    var categoriesDto = categoriesFromService.Select(category => new ListDocCategories
                    {
                        Id = category.Id,
                        Name = category.Name
                    }).ToList();

                    return View(new UpdateDocArticle { Categories = categoriesDto });
                }

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Index", "DocArticle", new { area = "AdminDashboard" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDocArticle(Guid articleId)
        {

            var token = HttpContext.Session.GetString("Token");

            await _articleService.DeleteDocArticleAsync(articleId, token);

            _notyf.Success("Makale Başarılı Şekilde Silindi.");

            return RedirectToAction("Index", "DocArticle", new { area = "AdminDashboard" });

        }
    }
}
