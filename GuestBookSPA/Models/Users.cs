﻿using System.ComponentModel.DataAnnotations;

namespace GuestBookSPA.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(string),
        ErrorMessageResourceName = "FieldIsRequired")]
        public string? Login { get; set; }
        [Required(ErrorMessageResourceType = typeof(string),
        ErrorMessageResourceName = "FieldIsRequired")]
        public string? Password { get; set; }

        public string? Salt { get; set; }
        public Users()
        {
            this.Messages = new HashSet<Messages>();
        }
        public ICollection<Messages>? Messages { get; set; }

    }
}