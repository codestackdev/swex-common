using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Diagnostics
{
    internal class TraceLogger : LoggerBase
    {
        internal TraceLogger(string category, bool logCallStack) : base(category, logCallStack)
        {
        }

        public override void Log(string msg)
        {
            System.Diagnostics.Trace.WriteLine(msg, Category);
        }
    }
}
