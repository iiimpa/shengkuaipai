using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.RequestParams
{
    public class OrderAddParams
    {
        //搜索端口
        public Platform platform { get; set; }
        //关键词
        public string keyword { get; set; }
        //地址
        public string domain { get; set; }
        //熊掌号
        public string xiongzhang { get; set; }
        //标题识别
        public string title { get; set; }
        //操作模式
        public Mode type { get; set; }
        //优化关键词
        public string optimization { get; set; }
        //订单天数
        public int days { get; set; }
        //固定点击次数
        public int fixed_click { get; set; }
        //点击方案
        public Dictionary<string, int> clickPlan { get; set; }
        //点击范围
        public Dictionary<string, int> clickLimtCount { get; set; }
        //跳转次数范围
        public Dictionary<string, int> randomJumpCount { get; set; }
        //停留时间范围
        public Dictionary<string, int> randomWaitCount { get; set; }
        //次数递增
        public Dictionary<string, int> increase { get; set; }
        //百分比递增
        public Dictionary<string, int> percentage { get; set; }
        //组编号
        public int group_id { get; set; }
    }
}
