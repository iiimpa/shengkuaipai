using WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class OrderListParams
    {
        //开始时间
        public string time_begin { get; set; }
        //结束时间
        public string time_over { get; set; }
        //当前页码
        public int pageindex { get; set; }
        //最大显示数量
        public int pagesize { get; set; }
        //关键词
        public string keyword { get; set; }
        //状态
        public int status { get; set; }
        //分组编号
        public int group_id { get; set; }
    }
}
