using System;
using System.ComponentModel.DataAnnotations;

namespace Wall.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage="Comment must be 5 or more characters")]
        [MaxLength(500, ErrorMessage="Comment must be 500 or less characters")]
        public string Content { get; set; }

        public int UserId { get; set; }
        public User Creator { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
