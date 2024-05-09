using Newtonsoft.Json;
using RobloxWithPinoo_UI.Entity.Dtos.CardDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using RobloxWithPinoo_UI.Entity.Messages;
using System.Net.Http.Headers;
using System.Text;

namespace RobloxWithPinoo_UI.Services.CardService
{
    public class CardService : ICardService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CardService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GeneralResult> CreateCardAsync(CreateCardDto createCardDto, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(createCardDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/Card/create-card", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    return new GeneralResult { Message = responseBody};
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new GeneralResult { Message = errorContent };
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

        public async Task<bool> DeleteCard(Guid cardId, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/Card/delete-card/{cardId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorContent);
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

        public async Task<List<CardListDto>> GetAllCardsByAppUser(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/Card/get-all-card-by-appuser");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CardListDto>>(content);
                }
                else
                {
                    return new List<CardListDto>();
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

        public async Task<List<CardListForAdminDto>> GetAllCardsForAdmin(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/Card/get-all-cards-for-admin");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CardListForAdminDto>>(content);
                }
                else
                {
                    return new List<CardListForAdminDto>();
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
