using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class GroupAddParams
    {
        //组名称
        public string group_name { get; set; }
        //组封面
        public string cover { get; set; }
        //组简介
        public string details { get; set; }
    }
}
