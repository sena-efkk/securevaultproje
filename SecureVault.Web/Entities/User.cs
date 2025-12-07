namespace SecureVault.Web.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        
        // Şifreleri asla düz metin tutmayız, hashlenmiş hali burada duracak.
        public string PasswordHash { get; set; } = string.Empty;
        
        // İleride yetkilendirme (Authorization) yaparken bunları kullanacağız.
        public string Role { get; set; } = "Employee"; 
        public string Department { get; set; } = "General"; 
        public int Seniority { get; set; } // Kıdem yılı
    }
}