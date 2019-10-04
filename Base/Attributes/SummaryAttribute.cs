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
    /// Decorates the description for the element (e.g. command, user control, object etc.)
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class SummaryAttribute : DescriptionAttribute
    {
        /// <summary>
        /// Constructor for element summary
        /// </summary>
        /// <param name="description">Description of the element</param>
        public SummaryAttribute(string description) : base(description)
        {
        }

        /// <inheritdoc cref="SummaryAttribute(string)"/>
        /// <param name="resType">Type of the static class (usually Resources)</param>
        /// <param name="descriptionResName">Resource name of the string for display name</param>
        public SummaryAttribute(Type resType, string descriptionResName)
            : this(ResourceHelper.GetResource<string>(resType, descriptionResName))
        {
        }
    }
}
