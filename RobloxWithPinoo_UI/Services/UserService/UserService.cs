using Newtonsoft.Json;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Entity.Dtos.UserDtos;
using System.Net.Http.Headers;

namespace RobloxWithPinoo_UI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ListUserDto>> GetAllUsers(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/User/get-all-users");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ListUserDto>>(content);
                }
                else
                {
                    return new List<ListUserDto>();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("HTTP isteği sırasında bir hata oluştu: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu: " + ex.Message);
            }
        }
    }
}
