using Microsoft.AspNetCore.Mvc;
using RobloxWithPinoo_UI.Entity.Dtos.CardDtos;
using RobloxWithPinoo_UI.Filters;
using RobloxWithPinoo_UI.Services.AccountService;
using RobloxWithPinoo_UI.Services.CardService;
using System.IdentityModel.Tokens.Jwt;

namespace RobloxWithPinoo_UI.Areas.UserDashboard.ViewComponents
{
    [TypeFilter(typeof(CheckUserTokenFilter))]
    public class CreateCardViewComponent : ViewComponent
    {
        private readonly ICardService _cardService;

        public CreateCardViewComponent(ICardService cardService)
        {
            _cardService = cardService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CreateCardDto createCardDto)
        {
            var token = HttpContext.Session.GetString("Token");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (string.IsNullOrEmpty(token) || (jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value != "User"))
            {
                return Content("Kullanıcı bulunamadı");
            }

            var createCard = await _cardService.CreateCardAsync(createCardDto,token);

            return View(createCard);
        }
    }
}
