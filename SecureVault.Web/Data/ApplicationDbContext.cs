using Microsoft.EntityFrameworkCore;
using SecureVault.Web.Entities;

namespace SecureVault.Web.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base (options)   
        {
            
        }
        public DbSet<User> Users{get;set;}
    }    

}