using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class LuaEditorController : Controller
    {
        public IActionResult Editor()
        {
            return View();
        }
    }
}
