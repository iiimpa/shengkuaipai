using System;
using WebApi.Helpers;

namespace WebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int UserId { get; set; }
        public Platform Platform { get; set; }
        public int PlanId { get; set; }
        public string Keyword { get; set; }
        public string Domain { get; set; }
        /// <summary>
        /// 下单时排名
        /// </summary>
        public int StartRank { get; set; }
        /// <summary>
        /// 当前排名
        /// </summary>
        public int CurrentRank { get; set; }
        /// <summary>
        /// 排名更新时间
        /// </summary>
        public DateTime RankTime { get; set; }
        public int Days { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum OrderStatus
    {
        Paied,
        Finish,
        Refund
    }
}