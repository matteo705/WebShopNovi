using WebShopNovi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebShopNovi.Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }
        
        public DbSet<Product> Products {  get; set; }    
        
    } 
}


//backup nize

//public class ApplicationDbContext : DbContext
//{
//    public ApplicationDbContext(DbContextOptions options) : base(options)
//    {

//    }

//    public DbSet<Product> Products { get; set; }

//}