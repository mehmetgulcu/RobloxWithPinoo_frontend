using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Services.AuthService;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
