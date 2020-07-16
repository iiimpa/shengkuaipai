using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class GetRankRequest
    {
        public string Domain { get; set; }
        public string Keyword { get; set; }
        public Platform Platform { get; set; }
    }
}
