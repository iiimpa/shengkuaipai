using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ClickPlan
    {
        //编号
        public int id { get; set; }
        //方案名称
        public string plan_name { get; set; }
        //创建人
        public int user_id { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //方案内容
        public string times { get; set; }
        //点击范围
        public string clickLimtCount { get; set; }
    }
}
