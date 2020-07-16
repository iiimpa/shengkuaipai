using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Group
    {
        //组编号
        public int id { get; set; }
        //组名称
        public string group_name { get; set; }
        //用户编号
        public int user_id { get; set; }
        //组封面
        public string cover { get; set; }
        //组简介
        public string details { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //更新时间
        public DateTime update_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
