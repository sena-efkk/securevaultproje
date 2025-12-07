using System.ComponentModel.DataAnnotations;

namespace SecureVault.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="kullanici adi zorunlu")]
        public string ?Username { get; set; }
        [Required(ErrorMessage ="sifre zorunlu")]
        [DataType(DataType.Password)]
        public string ?Password { get; set; }
    }
}