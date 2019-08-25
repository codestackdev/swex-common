//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using System.Text;

namespace CodeStack.SwEx.Common.Diagnostics
{
    /// <inheritdoc/>
    public abstract class LoggerBase : ILogger
    {
        public string Category
        {
            get;
            private set;
        }

        public bool LogStackTrace
        {
            get;
            private set;
        }

        protected LoggerBase(string category, bool logCallStack)
        {
            Category = category;
            LogStackTrace = logCallStack;
        }

        public void Log(Exception err)
        {
            var exMsg = new StringBuilder();
            ParseExceptionLog(err, exMsg, LogStackTrace);

            Log(exMsg.ToString());
        }

        public abstract void Log(string msg);

        /// <remarks>This call logs all exception (including inner) and call stack</remarks>
        protected void ParseExceptionLog(Exception ex, StringBuilder exMsg, bool logCallStack)
        {
            exMsg.AppendLine("Exception: " + ex?.Message
                + (logCallStack ? Environment.NewLine + ex?.StackTrace : ""));

            if (ex?.InnerException != null)
            {
                ParseExceptionLog(ex.InnerException, exMsg, logCallStack);
            }
        }
    }
}
