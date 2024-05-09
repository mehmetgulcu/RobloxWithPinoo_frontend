using AspNetCoreHero.ToastNotification.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Entity.Dtos.ActivationCodeDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.ActivationCodeService;
using RobloxWithPinoo_UI.Validators;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [TypeFilter(typeof(CheckAdminTokenFilter))]
    public class ActivationCodeController : Controller
    {
        private readonly IActivationCodeService _activationCodeService;
        private readonly INotyfService _notyf;

        public ActivationCodeController(IActivationCodeService activationCodeService, INotyfService notyf)
        {
            _activationCodeService = activationCodeService;
            _notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> ActivetedCodes()
        {
            var token = HttpContext.Session.GetString("Token");

            var values = await _activationCodeService.ActivatedStates(token);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> NotActivetedCodes()
        {
            var token = HttpContext.Session.GetString("Token");

            var values = await _activationCodeService.NotActivatedStates(token);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateActivationCode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateActivationCode(GenerateActivationCode generateActivationCode)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");

                var validator = new CreateActivationCodeValidator();
                var validationResult = validator.Validate(generateActivationCode);

                if (validationResult.IsValid)
                {
                    var result = await _activationCodeService.GenerateActivationCode(generateActivationCode, token);

                    _notyf.Success("Kod Başarılı Şekilde Oluşturuldu.");
                    return RedirectToAction("NotActivetedCodes", "ActivationCode", new { area = "AdminDashboard" });
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    return View(generateActivationCode);
                }

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("NotActivetedCodes", "ActivationCode", new { area = "AdminDashboard" });
            }
        }
    }
}
