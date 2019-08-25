//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.SwEx.Common.Attributes;
using CodeStack.SwEx.Common.Base;
using CodeStack.SwEx.Common.Reflection;
using System;
using System.ComponentModel;

namespace CodeStack.SwEx.Common.Diagnostics
{
    /// <summary>
    /// Factory for creating loggers
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// Creates a logger with specified parameters
        /// </summary>
        /// <param name="category">Logger category</param>
        /// <param name="logCallStack">True to log call stack</param>
        /// <param name="type">Type of the logger</param>
        /// <returns>New logger instance</returns>
        public static ILogger Create(string category = "SwEx", bool logCallStack = false,
            LoggerType_e type = LoggerType_e.Trace)
        {
            switch (type)
            {
                case LoggerType_e.Trace:
                    return new TraceLogger(category, logCallStack);
                default:
                    throw new NotSupportedException($"{type} logger is not supported");
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static ILogger Create(IModule module, string subModuleName = "")
        {
            return Create(module?.GetType(), subModuleName);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static ILogger Create(Type moduleType, string subModuleName = "")
        {
            if (moduleType == null)
            {
                throw new ArgumentNullException(nameof(moduleType));
            }

            if (!typeof(IModule).IsAssignableFrom(moduleType))
            {
                throw new InvalidCastException($"{moduleType.FullName} must implement {typeof(IModule).FullName}");
            }

            var logCallStack = false;
            var loggerType = LoggerType_e.Trace;
            var logName = moduleType.FullName;

            moduleType.TryGetAttribute<LoggerOptionsAttribute>(a =>
            {
                logCallStack = a.LogCallStack;
                loggerType = a.Type;
                logName = a.Name;
            });

            string moduleName = "";

            if (!moduleType.TryGetAttribute<ModuleInfoAttribute>(a => moduleName = a.Name))
            {
                throw new NullReferenceException($"{moduleType.FullName} doesn't have a {typeof(ModuleInfoAttribute).FullName} attribute");
            }

            var category = $"{moduleName}.{logName}{(string.IsNullOrEmpty(subModuleName) ? "" : "." + subModuleName)}";
            
            return Create(category, logCallStack, loggerType);
        }
    }
}
