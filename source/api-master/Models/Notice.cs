using System;

namespace WebApi.Models
{
    public class Notice
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Show { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}