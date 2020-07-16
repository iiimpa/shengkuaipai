using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class LoginLog
    {
        //日志编号
        public int id { get; set; }
        //用户编号
        public int user_id { get; set; }
        //登录IP
        public string login_ip { get; set; }
        //登录成功
        public bool success { get; set; }
        //登录时间
        public DateTime login_time { get; set; }
    }
}
