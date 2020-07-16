using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class RecoAddParams
    {
        //方案名称
        public string name { get; set; }
        //创建人
        public int user_id { get; set; }
        //点击方案编号
        public int click_id { get; set; }
        //停留方案编号
        public int stay_id { get; set; }
    }
}
