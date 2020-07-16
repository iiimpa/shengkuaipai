using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class RechargeParams
    {
        //充值方案编号
        [Required] public int Pid { get; set; }
        //充值回调
        [Required] public string Backurl { get; set; }
    }
}
