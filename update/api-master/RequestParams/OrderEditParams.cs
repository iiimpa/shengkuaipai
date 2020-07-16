using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.RequestParams
{
    public class OrderEditParams
    {
        //订单编号
        public int id { get; set; }
        //搜索端口
        public Platform platform { get; set; }
        //关键词
        public string keyword { get; set; }
        //地址
        public string domain { get; set; }
        //订单天数
        public int days { get; set; }
        //识别标题
        public string title { get; set; }
        //熊掌号
        public string xiongzhang { get; set; }
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
        //数量增加
        public Dictionary<string, int> increase { get; set; }
        //百分比增加
        public Dictionary<string, int> percentage { get; set; }
    }
}
