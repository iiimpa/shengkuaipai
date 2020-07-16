using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Models
{
    public class Task
    {
        //任务编号
        public int id { get; set; }
        //订单编号
        public int order_id { get; set; }
        //地址
        public string domain { get; set; }
        //关键词
        public string keyword { get; set; }
        //辅助关键词
        public string optimization { get; set; }
        //执行端口
        public Platform platform { get; set; }
        //操作模式
        public Mode mode { get; set; }
        //熊掌号
        public string xiongzhang { get; set; }
        //代理IP
        public string proxy_ip { get; set; }
        //请求头
        public string user_agent { get; set; }
        //屏幕分辨率
        public string resolution { get; set; }
        //预计消费
        public double cost { get; set; }
        //实际消费
        public double realcost { get; set; }
        //任务状态
        public Helpers.TaskStatus status { get; set; }
        //停留时间
        public string stay_time { get; set; }
        //跳转次数
        public int jump_times { get; set; }
        //调度时间
        public DateTime schedule_time { get; set; }
        //请求时间
        public DateTime request_time { get; set; }
        //完成时间
        public DateTime finish_time { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //更新时间
        public DateTime update_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
