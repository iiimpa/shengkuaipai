using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class RechargeAddParams
    {
        //方案名称
        public string name { get; set; }
        //数量
        public int amount { get; set; }
        //实际金币
        public double real_coin { get; set; }
        //赠送金币
        public double gift_coin { get; set; }
    }
}
