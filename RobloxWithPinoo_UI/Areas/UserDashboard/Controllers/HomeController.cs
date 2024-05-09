using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.Controllers
{
	[Area("UserDashboard")]
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class HomeController : Controller
	{
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
