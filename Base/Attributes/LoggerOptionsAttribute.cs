using CodeStack.SwEx.Common.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Attributes
{
    /// <summary>
    /// Specifies the options for the logger
    /// </summary>
    public class LoggerOptionsAttribute : Attribute
    {
        internal LoggerType_e Type { get; private set; }
        internal bool LogCallStack { get; private set; }
        internal string Name { get; private set; }

        /// <summary>
        /// Specifies the logger type and option to log call stack
        /// </summary>
        /// <param name="logCallStack">True to include call stack into the exception log message</param>
        /// <param name="name">Name of the logger</param>
        /// <param name="type">Type of logger</param>
        public LoggerOptionsAttribute(bool logCallStack, string name = "", LoggerType_e type = LoggerType_e.Trace)
        {
            Type = type;
            LogCallStack = logCallStack;
            Name = name;
        }
    }
}
