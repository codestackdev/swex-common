//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2018 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System.Collections.Generic;
using System.Drawing;

namespace CodeStack.SwEx.Common.Icons
{
    /// <summary>
    /// Represents the specific icon descriptor
    /// </summary>
    public interface IIcon
    {
        /// <summary>
        /// Transparency key to be applied to transparent color
        /// </summary>
        Color TransparencyKey { get; }

        /// <summary>
        /// List of required icon sizes
        /// </summary>
        /// <returns></returns>
        IEnumerable<IconSizeInfo> GetIconSizes();

        /// <summary>
        /// List of required icon size for high definition resolution
        /// </summary>
        /// <returns></returns>
        IEnumerable<IconSizeInfo> GetHighResolutionIconSizes();
    }
}
