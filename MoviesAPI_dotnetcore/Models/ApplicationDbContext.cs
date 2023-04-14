using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MoviesAPI_dotnetcore.Models
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
           
        }
        public DbSet<Categ> categs { get; set; } 
        public DbSet<Movie> Movies { get; set; }  
    }
}
