using RobloxWithPinoo_UI.Entity.Dtos.ContactFormDtos;

namespace RobloxWithPinoo_UI.Services.ContactFormService
{
    public interface IContactFormService
    {
        Task<bool> SubmitContactForm(SubmitContactFormDto submitContactFormDto);
        Task<List<AllContactForms>> GetAllUnReadContactForms(string token);
        Task<List<AllContactForms>> GetAllReadContactForms(string token);
        Task<bool> MakeReadContactForm(Guid formId, string token);
        Task<bool> DeleteContactForm(Guid formId, string token);
    }
}
