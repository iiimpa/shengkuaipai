using WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Order
    {
        //订单编号
        public int id { get; set; }
        //用户编号
        public int user_id { get; set; }
        //订单流水号
        public string serial_no { get; set; }
        //创建时间
        public DateTime create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //搜索端口
        public Platform platform { get; set; }
        //操作模式
        public Mode mode { get; set; }
        //标题识别
        public string title { get; set; }
        //辅助关键词
        public string optimization { get; set; }
        //关键词
        public string keyword { get; set; }
        //地址
        public string domain { get; set; }
        //熊掌号
        public string xiongzhang { get; set; }
        //订单天数
        public int days { get; set; }
        //订单状态
        public OrderStatus status { get; set; }
        //任务创建状态
        public CreateStatus create_status { get; set; }
        //更新时间
        public DateTime update_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //任务生成时间
        public DateTime task_create_time { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //初始排名
        public int start_rank { get; set; } = 100;
        //当前排名
        public int current_rank { get; set; } = 100;
        //订单消费
        public double amount { get; set; } = 0.00;
        //点击次数
        public string times { get; set; }
        //固定点击次数
        public int fixed_click { get; set; }
        //点击范围
        public string clickLimtCount { get; set; }
        //跳转次数范围
        public string randomJumpCount { get; set; }
        //停留时间范围
        public string randomWaitCount { get; set; }
        //次数递增
        public string increase { get; set; }
        //百分比递增
        public string percentage { get; set; }
        //组编号
        public int group_id { get; set; }
        //当日优化次数
        public int ipcount { get; set; } = 0;
        //指数
        public int point { get; set; } = 0;
    }
}
