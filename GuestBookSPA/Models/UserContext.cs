using Microsoft.EntityFrameworkCore;

namespace GuestBookSPA.Models
{
    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}