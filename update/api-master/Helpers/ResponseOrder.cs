using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class ResponseOrder: Models.Order
    {
        public int point { get; set; }
    }
}
