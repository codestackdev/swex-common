using CodeStack.SwEx.Common.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Base
{
    /// <summary>
    /// Utility for converting the different types of icons with an option to scale
    /// or generate different sets for high and low resolutions
    /// </summary>
    public interface IIconsConverter
    {
        /// <summary>
        /// Converts the group of icons and stacks them horizontally
        /// </summary>
        /// <param name="icons">List of icons to convert</param>
        /// <param name="highRes">True to generate high resolution icons</param>
        /// <returns>Full paths to generated icon images</returns>
        string[] ConvertIconsGroup(IIcon[] icons, bool highRes);

        /// <summary>
        /// Converts icon into the required size and quality and saves it on disk
        /// </summary>
        /// <param name="icon">Icon to convert</param>
        /// <param name="highRes">True to generate high resolution icon</param>
        /// <returns>Path to generated icons</returns>
        string[] ConvertIcon(IIcon icon, bool highRes);
    }
}
