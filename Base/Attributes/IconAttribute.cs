//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.SwEx.Common.Reflection;
using System;
using System.Drawing;

namespace CodeStack.SwEx.Common.Attributes
{
    /// <summary>
    /// General icon for any controls or objects
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class IconAttribute : Attribute
    {
        /// <summary>
        /// Image assigned to this icon
        /// </summary>
        public Image Icon { get; private set; }

        /// <param name="resType">Type of the static class (usually Resources)</param>
        /// <param name="masterResName">Resource name of the master icon</param>
        public IconAttribute(Type resType, string masterResName)
        {
            Icon = ResourceHelper.GetResource<Image>(resType, masterResName);
        }
    }
}
