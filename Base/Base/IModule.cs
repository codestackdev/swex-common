using CodeStack.SwEx.Common.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Base
{
    /// <summary>
    /// Base interface for SwEx modules
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Logger for this module
        /// </summary>
        ILogger Logger { get; }
    }
}
