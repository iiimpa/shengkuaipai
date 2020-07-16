using WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi.RequestParams
{
    public class TradeListParams
    {
        //交易类型
        public int status { get; set; }
        //开始时间
        public string time_begin { get; set; }
        //结束时间
        public string time_over { get; set; }
        //当前页码
        public int pageindex { get; set; }
        //最大显示条数
        public int pagesize { get; set; }
    }
}
