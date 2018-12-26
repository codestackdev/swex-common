using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Diagnostics
{
    /// <summary>
    /// Logs the trace messages
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log category
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="msg">Message</param>
        void Log(string msg);
    }

    /// <summary>
    /// Extension methods on logger
    /// </summary>
    public static class LoggerExtension
    {
        /// <summary>
        /// Logs exception
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="ex">Exception</param>
        /// <param name="logCallStack">True to log call stack</param>
        /// <remarks>This call logs all exception (including inner) and call stack</remarks>
        public static void Log(this ILogger logger, Exception ex, bool logCallStack = false)
        {
            var exMsg = new StringBuilder();
            ParseExceptionLog(ex, exMsg, logCallStack);

            logger.Log(exMsg.ToString());
        }

        private static void ParseExceptionLog(Exception ex, StringBuilder exMsg, bool logCallStack)
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
