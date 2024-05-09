using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
