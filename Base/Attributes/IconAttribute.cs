using CodeStack.SwEx.Common.Icons;
using CodeStack.SwEx.Common.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Attributes
{
    /// <summary>
    /// General icon for any controls or objects
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class IconAttribute : Attribute
    {
        public Image Icon { get; private set; }

        /// <param name="resType">Type of the static class (usually Resources)</param>
        /// <param name="masterResName">Resource name of the master icon</param>
        public IconAttribute(Type resType, string masterResName)
        {
            Icon = ResourceHelper.GetResource<Image>(resType, masterResName);
        }
    }
}
