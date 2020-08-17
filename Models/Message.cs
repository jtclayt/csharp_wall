using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wall.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage="Message must be 5 or more characters")]
        [MaxLength(500, ErrorMessage="Message must be 500 or less characters")]
        public string Content { get; set; }

        public int UserId { get; set; }
        public User Creator { get; set; }

        public List<Comment> Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
