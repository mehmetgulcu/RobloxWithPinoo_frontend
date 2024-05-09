using Newtonsoft.Json;
using RobloxWithPinoo_UI.Entity.Dtos.ActivationCodeDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Entity.Messages;
using System.Net.Http.Headers;
using System.Text;

namespace RobloxWithPinoo_UI.Services.ActivationCodeService
{
    public class ActivationCodeService : IActivationCodeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ActivationCodeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ActivationCodeListDto>> ActivatedStates(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ActivationCode/activated-states");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ActivationCodeListDto>>(content);
                }
                else
                {
                    return new List<ActivationCodeListDto>();
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

        public async Task<bool> GenerateActivationCode(GenerateActivationCode generateActivationCode, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(generateActivationCode);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ActivationCode/generate-activation-code", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return false;
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

        public async Task<List<ActivationCodeListDto>> NotActivatedStates(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ActivationCode/not-activated-states");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ActivationCodeListDto>>(content);
                }
                else
                {
                    return new List<ActivationCodeListDto>();
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
