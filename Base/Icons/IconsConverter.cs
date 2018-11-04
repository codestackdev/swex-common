//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2018 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.SwEx.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Icons
{
    /// <summary>
    /// Utility for converting the different types of icons with an option to scale
    /// or generate different sets for high and low resolutions
    /// </summary>
    public class IconsConverter : IDisposable
    {
        /// <summary>
        /// Icon data
        /// </summary>
        private class IconData
        {
            /// <summary>
            /// Source image in original format (not scaled, not modified)
            /// </summary>
            internal Image SourceIcon { get; set; }
            
            /// <summary>
            /// Required target size for the image
            /// </summary>
            internal Size TargetSize { get; set; }

            /// <summary>
            /// Path where the icon needs to be saved
            /// </summary>
            internal string TargetIconPath { get; private set; }

            internal IconData(string iconsDir, Image sourceIcon, Size targetSize, string name)
            {
                SourceIcon = sourceIcon;
                TargetSize = targetSize;
                TargetIconPath = Path.Combine(iconsDir, name);
            }
        }

        private readonly string m_IconsDir;
        private readonly bool m_DisposeIcons;

        public IconsConverter()
            : this(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()), true)
        {
        }
        
        /// <param name="iconsDir">Directory to store the icons</param>
        /// <param name="disposeIcons">True to remove the icons when class is disposed</param>
        public IconsConverter(string iconsDir,
            bool disposeIcons = true)
        {
            m_IconsDir = iconsDir;
            m_DisposeIcons = disposeIcons;

            if (!Directory.Exists(m_IconsDir))
            {
                Directory.CreateDirectory(m_IconsDir);
            }
        }

        /// <summary>
        /// Converts the group of icons and stacks them horizontally
        /// </summary>
        /// <param name="icons">List of icons to convert</param>
        /// <param name="highRes">True to generate high resolution icons</param>
        /// <returns>Full paths to generated icon images</returns>
        public string[] ConvertIconsGroup(IIcon[] icons, bool highRes)
        {
            if (icons == null || !icons.Any())
            {
                throw new ArgumentNullException(nameof(icons));
            }

            IconData[,] iconsDataGroup = null;

            var transparencyKey = icons.First().TransparencyKey;

            for (int i = 0; i < icons.Length; i++)
            {
                if (icons[i].TransparencyKey != transparencyKey)
                {
                    throw new IconTransparencyMismatchException(i);
                }

                var data = CreateIconData(icons[i], highRes);

                if (iconsDataGroup == null)
                {
                    iconsDataGroup = new IconData[data.Length, icons.Length];
                }

                for (int j = 0; j < data.Length; j++)
                {
                    iconsDataGroup[j, i] = data[j];
                }
            }

            var iconsPaths = new string[iconsDataGroup.GetLength(0)];

            for (int i = 0; i < iconsDataGroup.GetLength(0); i++)
            {
                var imgs = new Image[iconsDataGroup.GetLength(1)];
                for (int j = 0; j < iconsDataGroup.GetLength(1); j++)
                {
                    imgs[j] = iconsDataGroup[i, j].SourceIcon;
                }

                iconsPaths[i] = iconsDataGroup[i, 0].TargetIconPath;
                CreateBitmap(imgs, iconsPaths[i],
                    iconsDataGroup[i, 0].TargetSize, transparencyKey);
            }

            return iconsPaths;
        }

        /// <summary>
        /// Converts icon into the required size and quality and saves it on disk
        /// </summary>
        /// <param name="icon">Icon to convert</param>
        /// <param name="highRes">True to generate high resolution icon</param>
        /// <returns>Path to generated icons</returns>
        public string[] ConvertIcon(IIcon icon, bool highRes)
        {
            var iconsData = CreateIconData(icon, highRes);

            foreach (var iconData in iconsData)
            {
                CreateBitmap(new Image[] { iconData.SourceIcon },
                    iconData.TargetIconPath,
                    iconData.TargetSize, icon.TransparencyKey);
            }

            return iconsData.Select(i => i.TargetIconPath).ToArray();
        }

        private IconData[] CreateIconData(IIcon icon, bool highRes)
        {
            if (icon == null)
            {
                throw new ArgumentNullException(nameof(icon));
            }
            
            IEnumerable<IconSizeInfo> sizes = null;

            if (highRes)
            {
                sizes = icon.GetHighResolutionIconSizes();
            }
            else
            {
                sizes = icon.GetIconSizes();
            }

            if (sizes == null || !sizes.Any())
            {
                throw new NullReferenceException($"Specified icon '{icon.GetType().FullName}' doesn't provide any sizes");
            }

            var iconsData = sizes.Select(s => new IconData(m_IconsDir, s.SourceImage, s.TargetSize, s.Name)).ToArray();
            
            return iconsData;
        }

        private void CreateBitmap(Image[] sourceIcons,
            string targetIcon, Size size, Color background)
        {
            var width = size.Width * sourceIcons.Length;
            var height = size.Height;

            using (var bmp = new Bitmap(width,
                                    height, PixelFormat.Format24bppRgb))
            {
                using (var graph = Graphics.FromImage(bmp))
                {
                    graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graph.SmoothingMode = SmoothingMode.HighQuality;
                    graph.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var brush = new SolidBrush(background))
                    {
                        graph.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                    }

                    for (int i = 0; i < sourceIcons.Length; i++)
                    {
                        var sourceIcon = sourceIcons[i];

                        if (bmp.HorizontalResolution != sourceIcon.HorizontalResolution
                            || bmp.VerticalResolution != sourceIcon.VerticalResolution)
                        {
                            bmp.SetResolution(
                                sourceIcon.HorizontalResolution,
                                sourceIcon.VerticalResolution);
                        }

                        var widthScale = (double)size.Width / (double)sourceIcon.Width;
                        var heightScale = (double)size.Height / (double)sourceIcon.Height;
                        var scale = Math.Min(widthScale, heightScale);

                        int destX = 0;
                        int destY = 0;

                        if (heightScale < widthScale)
                        {
                            destX = (int)(size.Width - sourceIcon.Width * scale) / 2;
                        }
                        else
                        {
                            destY = (int)(size.Height - sourceIcon.Height * scale) / 2;
                        }

                        int destWidth = (int)(sourceIcon.Width * scale);
                        int destHeight = (int)(sourceIcon.Height * scale);

                        destX += i * size.Width;

                        graph.DrawImage(sourceIcon,
                            new Rectangle(destX, destY, destWidth, destHeight),
                            new Rectangle(0, 0, sourceIcon.Width, sourceIcon.Height),
                            GraphicsUnit.Pixel);
                    }
                }

                var dir = Path.GetDirectoryName(targetIcon);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                bmp.Save(targetIcon);
            }
        }

        public void Dispose()
        {
            if (m_DisposeIcons)
            {
                try
                {
                    Directory.Delete(m_IconsDir, true);
                }
                catch
                {
                }
            }
        }
    }
}
