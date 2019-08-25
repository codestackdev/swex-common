//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.SwEx.Common.Diagnostics;

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
