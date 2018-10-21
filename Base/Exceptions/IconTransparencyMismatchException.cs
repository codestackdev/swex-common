using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Exceptions
{
    public class IconTransparencyMismatchException : InvalidOperationException
    {
        public IconTransparencyMismatchException(int index)
            : base($"Transparency color of icon at index {index} doesn't match the group transparency")
        {
        }
    }
}
