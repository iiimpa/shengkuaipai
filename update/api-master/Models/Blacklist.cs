using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Blacklist
    {
        //编号
        public int id { get; set; }
        //用户编号
        public int user_id { get; set; }
        //进入时间
        public DateTime entry_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //进入原因
        public string entry_reason { get; set; }
    }
}
