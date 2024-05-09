using RobloxWithPinoo_UI.Entity.Dtos.UserDtos;

namespace RobloxWithPinoo_UI.Services.UserService
{
    public interface IUserService
    {
        Task<List<ListUserDto>> GetAllUsers(string token);
    }
}
