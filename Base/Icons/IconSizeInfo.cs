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
    public class IconSizeInfo
    {
        public static string CreateFileName(string baseName, Size targetSize)
        {
            if (string.IsNullOrEmpty(baseName))
            {
                baseName = Guid.NewGuid().ToString();
            }

            return $"{baseName}_{targetSize.Width}x{targetSize.Height}.bmp";
        }

        public Image SourceImage { get; private set; }
        public Size TargetSize { get; private set; }
        public string Name { get; private set; }

        public IconSizeInfo(Image srcImage, Size targetSize, string baseName = "")
        {
            SourceImage = srcImage;
            TargetSize = targetSize;

            Name = CreateFileName(baseName, targetSize);
        }
    }
}
