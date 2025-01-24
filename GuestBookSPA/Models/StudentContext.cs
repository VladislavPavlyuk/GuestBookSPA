using Microsoft.EntityFrameworkCore;

namespace GuestBookSPA.Models
{
    // Чтобы подключиться к базе данных через Entity Framework, необходим контекст данных. 
    // Контекст данных представляет собой класс, производный от класса DbContext.
    public class StudentContext : DbContext
    {
        public DbSet<Message> Students { get; set; }
        public StudentContext(DbContextOptions<StudentContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Students.Add(new Message { Name = "Максим", Surname = "Иваненко" });
                Students.Add(new Message {Name = "Ольга", Surname = "Алексеенко" });
                Students.Add(new Message {Name = "Сергей", Surname = "Петренко" });
                SaveChanges();
            }
        }
    }
}