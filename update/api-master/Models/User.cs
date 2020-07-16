using WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {
        //用户编号
        public int id { get; set; }
        //昵称
        public string nickname { get; set; }
        //账号
        public string account { get; set; }
        //密码
        public string password { get; set; }
        //电子邮箱
        public string email { get; set; }
        //QQ
        public string QQ { get; set; }
        //手机号
        public string telephone { get; set; }
        //用户身份
        public string roles { get; set; } = "operator";
        //用户等级
        public int level { get; set; } = 1;
        //积分
        public double coin { get; set; } = 0;
        //冻结积分
        public double frozen { get; set; }
        //冻结时间
        public DateTime frozen_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //待提现金额
        public double cash_out { get; set; } = 0.00;
        //已提现金额
        public double cash_in { get; set; } = 0.00;
        //单价
        public double price { get; set; } = 0.06;
        //头像
        public string avatar { get; set; } = "https://img.alicdn.com/imgextra/i1/2617887391/O1CN017c7N5u24T7v0Y7kFM_!!2617887391.png";
        //父级编号
        public int parent_id { get; set; }
        //私有地址
        public string url { get; set; }
        //支付宝账号
        public string alipay { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //上次登录时间
        public DateTime login_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //更新时间
        public DateTime update_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //代理用户量
        public int proxy_person_num { get; set; }
        //折扣
        public double discount { get; set; } = 0;
        //免费分析次数
        public int analysis_times { get; set; } = 2;
        //分析次数刷新时间
        public DateTime analysis_date { get; set; } = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"));
        //是否在黑名单
        public bool in_blacklist { get; set; } = false;
    }
}
