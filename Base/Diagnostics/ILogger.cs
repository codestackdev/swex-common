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
        /// Indicates if the messages should include call stack
        /// </summary>
        bool LogStackTrace { get; }

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="msg">Message</param>
        void Log(string msg);

        /// <summary>
        /// Logs error
        /// </summary>
        /// <param name="err">Exception</param>
        void Log(Exception err);
    }
}
