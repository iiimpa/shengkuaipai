using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class ClickAddParams
    {
        //方案名称
        public string plan_name { get; set; }
        //方案内容
        public int[] plan_content { get; set; }
        //点击次数范围
        public Dictionary<string,int> clickLimtCount { get; set; }
    }
}
