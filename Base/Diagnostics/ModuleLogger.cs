﻿using CodeStack.SwEx.Common.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CodeStack.SwEx.Common.Reflection;
using CodeStack.SwEx.Common.Attributes;

namespace CodeStack.SwEx.Common.Diagnostics
{
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class ModuleLogger : ILogger
    {
        public string Category { get; private set; }

        private readonly ILogger m_Logger;

        private readonly bool m_LogCallStack;

        public ModuleLogger(IModule module)
            : this(module?.GetType())
        {
        }

        public ModuleLogger(Type moduleType)
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

            Category = $"{moduleName}.{logName}";

            m_LogCallStack = logCallStack;
            m_Logger = LoggerFactory.CreateLogger(Category, loggerType);
        }

        public void Log(string msg)
        {
            m_Logger.Log(msg);
        }

        public void LogException(Exception ex)
        {
            m_Logger.Log(ex, m_LogCallStack);
        }
    }
}
