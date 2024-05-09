using RobloxWithPinoo_UI.Entity.Dtos.CardDtos;
using RobloxWithPinoo_UI.Entity.Messages;

namespace RobloxWithPinoo_UI.Services.CardService
{
    public interface ICardService
    {
        Task<GeneralResult> CreateCardAsync(CreateCardDto createCardDto, string token);
        Task<List<CardListDto>> GetAllCardsByAppUser(string token);
        Task<bool> DeleteCard(Guid cardId, string token);
        Task<List<CardListForAdminDto>> GetAllCardsForAdmin(string token);
    }
}
