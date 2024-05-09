using System.ComponentModel.DataAnnotations;

namespace RobloxWithPinoo_UI.Entity.Dtos.AuthDtos
{
	public class LoginDto
	{
        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
