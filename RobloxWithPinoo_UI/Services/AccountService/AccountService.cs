using Newtonsoft.Json;
using RobloxWithPinoo_UI.Entity.Dtos.AccountDtos;
using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace RobloxWithPinoo_UI.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AccountInfoDto> GetAccountInfoAsync(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/Account/get-account-info");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AccountInfoDto>(content);
                }
                else
                {
                    return new AccountInfoDto();
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
