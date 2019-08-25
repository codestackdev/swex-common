//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using CodeStack.SwEx.Common.Icons;

namespace CodeStack.SwEx.Common.Exceptions
{
    /// <summary>
    /// Exception indicates that the transparency key <see cref="IIcon.TransparencyKey"/> is different for
    /// some icons in the icons group passed to <see cref="IconsConverter.ConvertIconsGroup(IIcon[], bool)"/>
    /// </summary>
    public class IconTransparencyMismatchException : InvalidOperationException
    {
        public IconTransparencyMismatchException(int index)
            : base($"Transparency color of icon at index {index} doesn't match the group transparency")
        {
        }
    }
}
