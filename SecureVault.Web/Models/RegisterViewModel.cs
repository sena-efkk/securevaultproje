using System.ComponentModel.DataAnnotations;

namespace SecureVault.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(4, ErrorMessage = "Şifre en az 4 karakter olmalı.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")] // <--- İŞTE VIEWMODEL GÜCÜ
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}