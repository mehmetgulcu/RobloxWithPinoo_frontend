using System.ComponentModel.DataAnnotations;

namespace RobloxWithPinoo_UI.Entity.Dtos.AuthDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Eposta alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçersiz eposta adresi.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        public string Password { get; set; }
    }

}
