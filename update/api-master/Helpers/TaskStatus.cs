using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public enum TaskStatus
    {
        Wait,
        Running,
        NotFound,
        SurfaceError,
        Success,
        Failed
    }
}
