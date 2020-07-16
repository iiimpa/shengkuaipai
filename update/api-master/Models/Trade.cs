using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Models
{
    public class Trade
    {
        //交易编号
        public int id { get; set; }
        //交易类型
        public TradeType trade_type { get; set; }
        //用户编号
        public int user_id { get; set; }
        //提现账号
        public string account { get; set; }
        //流水号
        public string trade_no { get; set; }
        //方案编号
        public int plan_id { get; set; }
        //交易金额
        public double amount { get; set; }
        //交易状态
        public bool status { get; set; }
        //交易金币
        public double coin { get; set; }
        //说明
        public string description { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
