using System;
using System.ComponentModel.DataAnnotations;
using YYApi.Communications;

namespace WebApi.Community
{
    public class AdminEditConfigRequest
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class AdminUserInfoResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Account { get; set; }
    }

    public class AdminDashboardResponse : BaseResponse
    {
        public int AllUserCount { get; set; }
        public int AllAgentCount { get; set; }
        public int TodayRegisterCount { get; set; }
        public int TodayKeywordCount { get; set; }
        public decimal TodayRechargeCount { get; set; }
    }

    public class AdminUserEditRequest
    {
        [Required]
        public int Id { get; set; }
        public string Cell { get; set; }
        public string Password { get; set; }
        public int Coin { get; set; }
        public string Alipay { get; set; }
    }

    public class AdminKnowledgeListRequest
    {
        public string Category { get; set; }
    }
}
