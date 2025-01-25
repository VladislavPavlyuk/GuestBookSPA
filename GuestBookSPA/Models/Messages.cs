
namespace GuestBookSPA.Models
{
    public class Messages
    {   
        public int Id { get; set; }
        public required string MessageContent { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string Name { get; set; }

    }
}
