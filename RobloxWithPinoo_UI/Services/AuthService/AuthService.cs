using Newtonsoft.Json;
using RobloxWithPinoo_UI.Constants;
using RobloxWithPinoo_UI.Entity.Dtos.AuthDtos;
using RobloxWithPinoo_UI.Entity.Messages;
using System.Reflection.Metadata;
using System.Text;

namespace RobloxWithPinoo_UI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task LogoutAsync()
        {
            _httpContextAccessor.HttpContext.Session.Remove("Token");

            return Task.CompletedTask;
        }

        public async Task<LoginResult> LoginUserAsync(LoginDto loginDto)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                var response = await client.PostAsJsonAsync($"{BaseUrl.BackendBaseUrl}/api/Auth/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeAnonymousType(content, new { Token = "" });
                    var token = tokenResponse.Token;

                    if (!string.IsNullOrEmpty(token))
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("Token", token);
                        return new LoginResult { Token = token };
                    }
                    else
                    {
                        return new LoginResult { ErrorMessage = "Geçersiz token alındı." };
                    }
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new LoginResult { ErrorMessage = errorMessage };
                }
            }
            catch (Exception ex)
            {
                return new LoginResult { ErrorMessage = $"API'den hata döndü: {ex.Message}" };
            }
        }


        public async Task<GeneralResult> RegisterUserAsync(RegisterDto registerDto)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                var response = await client.PostAsJsonAsync($"{BaseUrl.BackendBaseUrl}/api/Auth/register", registerDto);

                if (response.IsSuccessStatusCode)
                {
                    return new GeneralResult { Message = "Kayıt Başarılı" };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new GeneralResult { Message = errorMessage };
                }
            }
            catch (Exception ex)
            {
                return new GeneralResult { Message = $"API'den hata döndü: {ex.Message}" };

            }
        }
    }
}
