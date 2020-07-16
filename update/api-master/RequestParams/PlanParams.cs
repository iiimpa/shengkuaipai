using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class PlanParams
    {
        public int id { get; set; }
        public int pagesize { get; set; }
        public int pageindex { get; set; }
    }
}
