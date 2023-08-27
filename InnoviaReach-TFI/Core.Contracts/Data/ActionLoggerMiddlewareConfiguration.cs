using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Data
{
    public class ActionLoggerMiddlewareConfiguration
    {
        public bool ReadRequestBody { get; set; }
        public int MaximumLength { get; set; }
    }
}
