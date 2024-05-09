using RobloxWithPinoo_UI.Entity.Dtos.AuthDtos;
using RobloxWithPinoo_UI.Entity.Messages;

namespace RobloxWithPinoo_UI.Services.AuthService
{
	public interface IAuthService
	{
        Task<LoginResult> LoginUserAsync(LoginDto loginDto);
		Task<GeneralResult> RegisterUserAsync(RegisterDto registerDto);
        Task LogoutAsync();
    }
}
