//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2018 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Icons
{
    public interface IIcon
    {
        IEnumerable<IconSizeInfo> GetIconSizes();
        IEnumerable<IconSizeInfo> GetHighResolutionIconSizes();
    }
}
