using AspNetCoreHero.ToastNotification.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Entity.Dtos.ContactFormDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Services.ContactFormService;
using RobloxWithPinoo_UI.Validators;

namespace RobloxWithPinoo_UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactFormService _contactFormService;
        private readonly INotyfService _notyf;

        public ContactController(IContactFormService contactFormService, INotyfService notyf)
        {
            _contactFormService = contactFormService;
            _notyf = notyf;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "İletişim";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SubmitContactFormDto submitContactFormDto)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");

                var validator = new SubmitContactFormValidator();
                var validationResult = validator.Validate(submitContactFormDto);

                if (validationResult.IsValid)
                {
                    var result = await _contactFormService.SubmitContactForm(submitContactFormDto);

                    _notyf.Success("Mesajınız başarıyla gönderildi.");
                    return RedirectToAction("Index", "Contact", new { area = "" });
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }

                    return View(submitContactFormDto);
                }

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Index", "Contact", new { area = "" });
            }
        }
    }
}
