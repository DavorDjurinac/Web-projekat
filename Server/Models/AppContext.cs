using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class ContextKlasa : DbContext
    {
        public DbSet<Hotel> Hoteli {get; set;} 
        public DbSet<Soba> Sobe {get; set;}
        public DbSet<Racun> Racuni {get; set;}
        public ContextKlasa(DbContextOptions options) : base(options)
        {

        }
    }
}