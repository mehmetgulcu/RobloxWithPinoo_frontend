using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.DocArticleService;
using RobloxWithPinoo_UI.Services.DocCategoryService;
using RobloxWithPinoo_UI.Validators;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [TypeFilter(typeof(CheckAdminTokenFilter))]
    public class DocCategoryController : Controller
    {
        private readonly IDocArticleService _articleService;
        private readonly IDocCategoryService _categoryService;
        private readonly INotyfService _notyf;

        public DocCategoryController(IDocArticleService articleService, IDocCategoryService categoryService, INotyfService notyf)
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

            var listCategory = await _categoryService.GetDocCategoriesForAllUsers(token);

            return View(listCategory);
        }

        [HttpGet]
        public async Task<IActionResult> CreateDocCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocCategory(CreateDocCategory createDocCategory)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");

                var validator = new CreateDocCategoryValidator();
                var validationResult = validator.Validate(createDocCategory);

                if (validationResult.IsValid)
                {
                    var result = await _categoryService.CreateDocCategory(createDocCategory, token);

                    _notyf.Success("Kategori Başarılı Şekilde Oluşturuldu.");
                    return RedirectToAction("Index", "DocCategory", new { area = "AdminDashboard" });
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    return View(createDocCategory);
                }

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Index", "DocCategory", new { area = "AdminDashboard" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDocCategory(Guid categoryId)
        {
            var token = HttpContext.Session.GetString("Token");

            var category = await _categoryService.GetDocCategoryByIdAsync(categoryId, token);

            if (category == null)
            {
                return RedirectToAction("Index", "DocCategory", new { area = "AdminDashboard" });
            }

            var categoryUpdateDto = new UpdateDocCategory
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(categoryUpdateDto);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDocCategory(UpdateDocCategory updateDocCategory)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");

                var validator = new UpdateDocCategoryValidator();
                var validationResult = validator.Validate(updateDocCategory);

                if (validationResult.IsValid)
                {
                    var result = await _categoryService.UpdateDocCategory(updateDocCategory, updateDocCategory.Id, token);

                    _notyf.Success("Kategori Başarılı Şekilde Güncellendi.");
                    return RedirectToAction("Index", "DocCategory", new { area = "AdminDashboard" });
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    return View(updateDocCategory);
                }

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Index", "DocCategory", new { area = "AdminDashboard" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDocCategory(Guid categoryId)
        {

            var token = HttpContext.Session.GetString("Token");

            await _categoryService.DeleteDocCategoryAsync(categoryId, token);

            _notyf.Success("Kategori Başarılı Şekilde Silindi.");

            return RedirectToAction("Index", "DocCategory", new { area = "AdminDashboard" });
        }
    }
}
