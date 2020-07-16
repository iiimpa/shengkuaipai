using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Helpers;
using WebApi.Models;

namespace Community
{
    public class UserOrderAddRequest
    {
        [Required]
        public string Keyword { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public Platform Platform { get; set; }
        [Required]
        public int Plan { get; set; }
        [Required]
        public int[] Time { get; set; }
        [Required]
        public int Raise { get; set; }
        [Required]
        public int Days { get; set; }
        [Required]
        public int Rank { get; set; }
    }

    public class UserTaskListRequest
    {
        [Required]
        public int OrderId { get; set; }
    }

    public class GetRankRequest
    {
        [Required]
        public Platform Platform { get; set; }
        [Required]
        public string Keyword { get; set; }
        [Required]
        public string Domain { get; set; }
    }

    public class UserChangePasswordRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }

    public class UserChangeAlipayRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Alipay { get; set; }
    }

    public class UserGetDownRequest
    {
        [Required]
        public int Id { get; set; }
    }

    public class UserGetTradeRequest
    {
        public TradeTypeEnum Type { get; set; }
    }

    public class UserOnlyIdRequest
    {
        public int Id { get; set; }
    }

    public class UserRechargeRequest
    {
        [Required]
        public int Rid { get; set; }
        [Required]
        public string BackUrl { get; set; }
    }
}
