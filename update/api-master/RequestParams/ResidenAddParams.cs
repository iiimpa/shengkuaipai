using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class ResidenAddParams
    {
        //方案名称
        [Required] public string name { get; set; }
        //停留时间范围
        [Required] public Dictionary<string, int> randomWaitCount { get; set; }
        //跳转次数范围
        [Required] public Dictionary<string, int> randomJumpCount { get; set; }
        //方案价格
        [Required] public double price { get; set; }
    }
}
