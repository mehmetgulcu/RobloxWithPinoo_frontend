using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [TypeFilter(typeof(CheckAdminTokenFilter))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
