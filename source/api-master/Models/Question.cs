using System;

namespace WebApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}