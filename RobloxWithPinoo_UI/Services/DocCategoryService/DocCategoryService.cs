using Newtonsoft.Json;
using RobloxWithPinoo_UI.Entity.Dtos.AccountDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocArticleDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using System.Net.Http.Headers;
using System.Text;

namespace RobloxWithPinoo_UI.Services.DocCategoryService
{
    public class DocCategoryService : IDocCategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DocCategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateDocCategory(CreateDocCategory createDocCategory, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(createDocCategory);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/DocCategory/create-doc-category", content);

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

        public async Task<bool> DeleteDocCategoryAsync(Guid categoryId, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/DocCategory/delete-doc-category/{categoryId}");

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

        public async Task<List<ListDocCategories>> GetDocCategoriesForAllUsers(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/DocCategory/get-doc-category-for-all-users");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ListDocCategories>>(content);
                }
                else
                {
                    return new List<ListDocCategories>();
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

        public async Task<DocCategory> GetDocCategoryByIdAsync(Guid categoryId, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/DocCategory/get-category-by-id/{categoryId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DocCategory>(content);
                }
                else
                {
                    return null;
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

        public async Task<bool> UpdateDocCategory(UpdateDocCategory updateDocCategory, Guid categoryId, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(updateDocCategory);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/DocCategory/update-doc-category/{categoryId}", content);

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
    }
}
