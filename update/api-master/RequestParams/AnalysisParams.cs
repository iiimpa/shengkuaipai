using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.RequestParams
{
    public class AnalysisParams
    {
        public Helpers.Platform platform { get; set; }
        public string url { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
}
