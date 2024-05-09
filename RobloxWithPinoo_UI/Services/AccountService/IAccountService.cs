using RobloxWithPinoo_UI.Entity.Dtos.AccountDtos;

namespace RobloxWithPinoo_UI.Services.AccountService
{
    public interface IAccountService
    {
        Task<AccountInfoDto> GetAccountInfoAsync(string token);
    }
}
