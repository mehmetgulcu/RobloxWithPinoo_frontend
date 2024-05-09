using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RobloxWithPinoo_UI.Entity.Dtos.ActivationCodeDtos;
using RobloxWithPinoo_UI.Entity.Dtos.AuthDtos;
using RobloxWithPinoo_UI.Services.AuthService;
using RobloxWithPinoo_UI.Validators;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly INotyfService _notyf;

        public AuthController(IAuthService authService, INotyfService notyf)
        {
            _authService = authService;
            _notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var validator = new LoginValidator();
                var validationResult = validator.Validate(loginDto);

                if (validationResult.IsValid)
                {
                    var loginResult = await _authService.LoginUserAsync(loginDto);

                    if (loginResult.Token != null)
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(loginResult.Token) as JwtSecurityToken;
                        var roles = jsonToken.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

                        if (roles.Contains("User"))
                        {
                            if (roles.Contains("Admin"))
                            {
                                return View(loginDto);
                            }

                            return RedirectToAction("Index", "Home", new { area = "UserDashboard" });
                        }
                        else if (roles.Contains("Admin"))
                        {
                            if (roles.Contains("User"))
                            {
                                return View(loginDto);
                            }

                            return RedirectToAction("Index", "Home", new { area = "AdminDashboard" });
                        }
                    }
                    else
                    {
                        _notyf.Error("Oturum açılamadı.");
                        return RedirectToAction("Login", "Auth", new { area = "" });
                    }

                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    return View(loginDto);
                }

                _notyf.Error("Oturum açılamadı.");
                return RedirectToAction("Login", "Auth", new { area = "" });

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Login", "Auth", new { area = "" });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var validator = new RegisterValidator();
                var validationResult = validator.Validate(registerDto);

                if (validationResult.IsValid)
                {
                    if (ModelState.IsValid)
                    {
                        var result = await _authService.RegisterUserAsync(registerDto);

                        if (result.Message == "Kayıt Başarılı")
                        {
                            return RedirectToAction("SuccessRegisterPage", "Auth", new { area = "" });
                        }
                        else
                        {
                            _notyf.Error("Opss! Bir şeyler eksik gitti.");
                            return RedirectToAction("Register", "Auth", new { area = "" });
                        }
                    }
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    return View(registerDto);
                }

                _notyf.Error("Başarısız kayıt.");
                return RedirectToAction("Register", "Auth", new { area = "" });
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Register", "Auth", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SuccessRegisterPage()
        {
            return View();
        }
    }
}
