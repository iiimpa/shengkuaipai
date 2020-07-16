using System;
namespace WebApi.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Times { get; set; }
        public int Price { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
