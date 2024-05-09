using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.ContactFormService;

namespace RobloxWithPinoo_UI.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [TypeFilter(typeof(CheckAdminTokenFilter))]
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
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");

            var forms = await _contactFormService.GetAllUnReadContactForms(token);

            return View(forms);
        }

        [HttpGet]
        public async Task<IActionResult> ReadContactForms()
        {
            var token = HttpContext.Session.GetString("Token");

            var forms = await _contactFormService.GetAllReadContactForms(token);

            return View(forms);
        }

        [HttpGet]
        public async Task<IActionResult> MakeReadForm(Guid formId)
        {
            var token = HttpContext.Session.GetString("Token");

            await _contactFormService.MakeReadContactForm(formId, token);

            _notyf.Success("Form Başarılı Şekilde Okundu.");

            return RedirectToAction("Index", "Contact", new { area = "AdminDashboard" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid formId)
        {
            var token = HttpContext.Session.GetString("Token");

            await _contactFormService.DeleteContactForm(formId, token);

            _notyf.Success("Form Başarılı Şekilde Silindi.");

            return RedirectToAction("Index", "Contact", new { area = "AdminDashboard" });
        }
    }
}
