using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ResidenPlan
    {
        //编号
        public int id { get; set; }
        //创建人
        public int user_id { get; set; }
        //方案名称
        public string name { get; set; }
        //停留时间范围
        public string randomWaitCount { get; set; }
        //跳转次数范围
        public string randomJumpCount { get; set; }
        //方案价格
        public double price { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
