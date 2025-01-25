using Microsoft.EntityFrameworkCore;

namespace GuestBookSPA.Models
{
    public class GuestBookContext : DbContext
    {
        public DbSet<Messages> Messages { get; set; }
        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {                
                SaveChanges();
            }
        }
    }
}