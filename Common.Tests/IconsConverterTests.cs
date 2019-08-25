using CodeStack.SwEx.Common.Icons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
    [TestClass]
    public class IconsConverterTests
    {
        [TestMethod]
        public void ReplaceColor()
        {
            var bmp = new Bitmap(2, 2);
            bmp.SetPixel(0, 0, Color.FromArgb(255, 100, 50));
            bmp.SetPixel(0, 1, Color.FromArgb(255, 1, 1));
            bmp.SetPixel(1, 0, Color.FromArgb(255, 100, 50));
            bmp.SetPixel(1, 1, Color.FromArgb(255, 2, 2));

            var res = IconsConverter.ReplaceColor(bmp,
                new IconsConverter.ColorReplacerDelegate((ref byte r, ref byte g, ref byte b, ref byte a) => 
                {
                    if (r == 255 && g == 100 && b == 50)
                    {
                        r = 10;
                        g = 20;
                        b = 30;
                    }
                })) as Bitmap;

            Assert.AreNotEqual(bmp, res);
            Assert.AreEqual(Color.FromArgb(10, 20, 30), res.GetPixel(0, 0));
            Assert.AreEqual(Color.FromArgb(255, 1, 1), res.GetPixel(0, 1));
            Assert.AreEqual(Color.FromArgb(10, 20, 30), res.GetPixel(1, 0));
            Assert.AreEqual(Color.FromArgb(255, 2, 2), res.GetPixel(1, 1));
        }
    }
}
