//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2018 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using System.Drawing;

namespace CodeStack.SwEx.Common.Icons
{
    /// <summary>
    /// Descriptor for the icon of a specific type
    /// </summary>
    public class IconSizeInfo
    {
        /// <summary>
        /// Generates the file name for the icon
        /// </summary>
        /// <param name="baseName">Base name for the icon</param>
        /// <param name="targetSize">Required icon size</param>
        /// <returns>Suggested file name</returns>
        public static string CreateFileName(string baseName, Size targetSize)
        {
            if (string.IsNullOrEmpty(baseName))
            {
                baseName = Guid.NewGuid().ToString();
            }

            return $"{baseName}_{targetSize.Width}x{targetSize.Height}.bmp";
        }

        /// <summary>
        /// Original image of the icon
        /// </summary>
        public Image SourceImage { get; private set; }

        /// <summary>
        /// Required size of the icon
        /// </summary>
        public Size TargetSize { get; private set; }

        /// <summary>
        /// Base name of the icon
        /// </summary>
        public string Name { get; private set; }

        public IconSizeInfo(Image srcImage, Size targetSize, string baseName = "")
        {
            SourceImage = srcImage;
            TargetSize = targetSize;

            Name = CreateFileName(baseName, targetSize);
        }
    }
}
