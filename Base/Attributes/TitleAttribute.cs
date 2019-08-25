//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.SwEx.Common.Reflection;
using System;
using System.ComponentModel;

namespace CodeStack.SwEx.Common.Attributes
{
    /// <summary>
    /// Decorates the display name for the element (e.g. command, user control, object etc.)
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class TitleAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// Constructor for element title
        /// </summary>
        /// <param name="dispName">Display name of the element</param>
        public TitleAttribute(string dispName) : base(dispName)
        {
        }

        /// <inheritdoc cref="TitleAttribute(string)"/>
        /// <param name="resType">Type of the static class (usually Resources)</param>
        /// <param name="dispNameResName">Resource name of the string for display name</param>
        public TitleAttribute(Type resType, string dispNameResName)
            : this(ResourceHelper.GetResource<string>(resType, dispNameResName))
        {
        }
    }
}
