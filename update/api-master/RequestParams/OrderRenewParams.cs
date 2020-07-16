using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class OrderRenewParams
    {
        //订单编号
        public int order_id { get; set; }
        //续费天数
        public int days { get; set; }
    }
}
