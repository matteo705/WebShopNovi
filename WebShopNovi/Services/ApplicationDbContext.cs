using WebShopNovi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebShopNovi.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }
        
        public DbSet<Product> Products {  get; set; }    
        
    }
}
