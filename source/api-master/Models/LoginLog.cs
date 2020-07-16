using System;

namespace WebApi.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public bool Success { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}