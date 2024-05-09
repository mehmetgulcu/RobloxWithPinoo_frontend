using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RobloxWithPinoo_UI.Entity.Dtos.ContactFormDtos;
using RobloxWithPinoo_UI.Entity.Dtos.DocCategoryDtos;
using System.Net.Http.Headers;
using System.Text;

namespace RobloxWithPinoo_UI.Services.ContactFormService
{
    public class ContactFormService : IContactFormService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactFormService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> DeleteContactForm(Guid formId, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ContactForm/delete-contact-forms/{formId}");

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

        public async Task<List<AllContactForms>> GetAllReadContactForms(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ContactForm/get-all-read-contact-forms");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<AllContactForms>>(content);
                }
                else
                {
                    return new List<AllContactForms>();
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

        public async Task<List<AllContactForms>> GetAllUnReadContactForms(string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ContactForm/get-all-un-read-contact-forms");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<AllContactForms>>(content);
                }
                else
                {
                    return new List<AllContactForms>();
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

        public async Task<bool> MakeReadContactForm(Guid formId, string token)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PatchAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ContactForm/make-read-contact-forms/{formId}",null);

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

        public async Task<bool> SubmitContactForm(SubmitContactFormDto submitContactFormDto)
        {
            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                });

                var jsonContent = JsonConvert.SerializeObject(submitContactFormDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{Constants.BaseUrl.BackendBaseUrl}/api/ContactForm/submit", content);

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
    }
}
