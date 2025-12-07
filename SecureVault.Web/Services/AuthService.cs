using SecureVault.Web.Data;
using SecureVault.Web.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SecureVault.Web.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        
    
        public async Task<User?> ValidateUser(string username, string password)
        {
            var user =await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            // 2. verify metodu: Gelen şifreyi hash'le, veritabanındakiyle kıyasla.
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (isValid) return user;
            return null;
        }


        public void RegisterUser(string username, string password, string role)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                return ;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = passwordHash, 
                Role = role,
                Department = "IT", //paraemetre olarak alınması sonraan departmen yada başka bişey değişirse patlar
                Seniority = 1
            };
            
            _context.Users.Add(newUser);
            _context.SaveChanges();
            
         }
    }
}