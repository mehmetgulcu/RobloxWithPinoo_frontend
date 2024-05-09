using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.CloudinaryService;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [TypeFilter(typeof(CheckAdminTokenFilter))]
    public class ImageStorageController : Controller
    {
        private readonly ICloudinaryService _cloudinaryService;

        public ImageStorageController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var imageUrls = await _cloudinaryService.ListImagesAsync();
            return View(imageUrls);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(file);

                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
