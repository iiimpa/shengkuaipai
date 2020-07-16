using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Trade
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(10)")]
        public TradeTypeEnum Type { get; set; }
        public int UserId { get; set; }
        public string TradeNo { get; set; }
        public int Coin { get; set; }
        public decimal Amount { get; set; }
        public int RelationId { get; set; }
        public string Description { get; set; }
        [DefaultValue(0)]
        public int Status { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum TradeTypeEnum
    {
        Recharge,
        Withdraw,
        Refund,
        Cost,
        Commission
    }
}
