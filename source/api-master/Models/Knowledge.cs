using System;

namespace WebApi.Models
{
    public class Knowledge
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}