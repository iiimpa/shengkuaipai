using System;
namespace WebApi.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Domain { get; set; }
        public int Origin { get; set; }
        public int Now { get; set; }
        public int Up { get; set; }
        public string Date { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
