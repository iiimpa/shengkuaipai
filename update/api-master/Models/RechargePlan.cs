using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class RechargePlan
    {
        //编号
        public int id { get; set; }
        //方案名称
        public string name { get; set; }
        //图片地址
        public string url { get; set; }
        //金额
        public double amount { get; set; }
        //实际金币
        public double real_coin { get; set; }
        //赠送金币
        public double gift_coin { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
