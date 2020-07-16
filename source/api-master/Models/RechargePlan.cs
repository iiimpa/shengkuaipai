using System;
namespace WebApi.Models
{
    public class RechargePlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int RealCoin { get; set; }
        public int GiftCoin { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
