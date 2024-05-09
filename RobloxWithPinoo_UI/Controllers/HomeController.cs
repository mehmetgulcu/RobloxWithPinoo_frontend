using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Models;
using System.Diagnostics;

namespace RobloxWithPinoo_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Faq()
        {
            ViewBag.PageTitle = "Sýkça Sorulan Sorular";
            return View();
        }
        
        public IActionResult PrivacyPolicy()
        {
            ViewBag.PageTitle = "Gizlilik Sözleþmesi";
            return View();
        }

        public IActionResult KVKK()
        {
            ViewBag.PageTitle = "KVKK-Aydýnlatma Metni";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
