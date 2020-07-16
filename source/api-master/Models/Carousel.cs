using System;
namespace WebApi.Models
{
    public class Carousel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
