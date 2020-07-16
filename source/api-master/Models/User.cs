using System;
using System.ComponentModel;

namespace WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [DefaultValue(0)]
        public int Pid { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Cell { get; set; }
        public int Coin { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalRecharge { get; set; } = 0;
        public int IsAgent { get; set; } = 0;
        public int Level { get; set; } = 0;
        public string Alipay { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}