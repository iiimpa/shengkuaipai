using System;

namespace WebApi.Models
{
    public class Config
    {
        public int Id { get; set; }
        public string Descirption { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}