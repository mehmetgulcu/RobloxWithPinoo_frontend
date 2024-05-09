using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Entity.Dtos.CardDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.CardService;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly INotyfService _notyf;

        public CardController(ICardService cardService, INotyfService notyf)
        {
            _cardService = cardService;
            _notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");

            var cards = await _cardService.GetAllCardsByAppUser(token);

            return View(cards);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(CreateCardDto createCardDto)
        {
            try
            {
                var token = HttpContext.Session.GetString("Token");

                var result = await _cardService.CreateCardAsync(createCardDto, token);

                if(result.Message == "Başarılı")
                {
                    _notyf.Success("Kart başarıyla oluşturuldu.");
                    return RedirectToAction("Index", "Card", new { area = "UserDashboard" });
                }
                else
                {
                    if (result.Message == "Böyle bir kart zaten var.")
                    {
                        _notyf.Error("Böyle bir kart zaten var.");
                    }
                    else if (result.Message == "Aktivasyon kodu geçersiz veya zaten kullanılmış.")
                    {
                        _notyf.Error("Aktivasyon kodu geçersiz veya zaten kullanılmış.");
                    }
                    else if (result.Message == "Kullanıcı bulunamadı.")
                    {
                        _notyf.Error("Kullanıcı bulunamadı.");
                    }
                    else if (result.Message == "Kart adı boşluk veya Türkçe karakter içeremez.")
                    {
                        _notyf.Error("Kart adı boşluk veya Türkçe karakter içeremez.");
                    }
                    
                    return View(createCardDto);
                }
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                _notyf.Error("Sunucuyla iletişim sırasında bir hata oluştu, lütfen tekrar deneyin.");
                return RedirectToAction("Index", "Card", new { area = "UserDashboard" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCard(Guid cardId)
        {

            var token = HttpContext.Session.GetString("Token");

            await _cardService.DeleteCard(cardId, token);

            return RedirectToAction("Index", "Card", new { area = "UserDashboard" });
        }
    }
}
