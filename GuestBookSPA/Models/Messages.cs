
using GuestBookSPA.Models;
using System.ComponentModel.DataAnnotations;

namespace GuestBookSPA.Models
{
    public class Messages
    {   
        public int Id { get; set; }
        public required string Message { get; set; }
        public DateTime MessageDateTime { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; }
        
    }
}
