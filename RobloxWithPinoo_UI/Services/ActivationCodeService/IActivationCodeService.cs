using RobloxWithPinoo_UI.Entity.Dtos.ActivationCodeDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Entity.Messages;

namespace RobloxWithPinoo_UI.Services.ActivationCodeService
{
    public interface IActivationCodeService
    {
        Task<bool> GenerateActivationCode(GenerateActivationCode generateActivationCode, string token);
        Task<List<ActivationCodeListDto>> ActivatedStates(string token);
        Task<List<ActivationCodeListDto>> NotActivatedStates(string token);
    }
}
