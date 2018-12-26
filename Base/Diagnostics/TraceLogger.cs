using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Diagnostics
{
    internal class TraceLogger : ILogger
    {
        public string Category { get; private set; }

        internal TraceLogger(string category)
        {
            Category = category;
        }

        public void Log(string msg)
        {
            System.Diagnostics.Trace.WriteLine(msg, Category);
        }
    }
}
