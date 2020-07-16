using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class TaskListParams
    {
        //订单编号
        public int order_id { get; set; }
        //开始时间
        public string time_begin { get; set; }
        //结束时间
        public string time_over { get; set; }
        //当前页码
        public int pageindex { get; set; }
        //最大显示数量
        public int pagesize { get; set; }
    }
}
