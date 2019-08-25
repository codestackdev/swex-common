//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

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
