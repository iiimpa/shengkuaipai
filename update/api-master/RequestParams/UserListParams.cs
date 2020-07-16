using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class UserListParams
    {
        public int id { get; set; }
        //拉取类型
        public int type { get; set; }
        //当前页码
        public int pageindex { get; set; }
        //最大显示条数
        public int pagesize { get; set; }
    }
}
