using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Diagnostics
{
    public static class LoggerFactory
    {
        public static ILogger CreateLogger(string category = "SwEx",
            LoggerType_e type = LoggerType_e.Trace)
        {
            switch (type)
            {
                case LoggerType_e.Trace:
                    return new TraceLogger(category);
                default:
                    throw new NotSupportedException($"{type} logger is not supported");
            }
        }
    }
}
