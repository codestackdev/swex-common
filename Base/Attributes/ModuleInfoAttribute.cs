using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.Common.Attributes
{
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class ModuleInfoAttribute : Attribute
    {
        internal string Name { get; private set; }

        public ModuleInfoAttribute(string name)
        {
            Name = name;
        }
    }
}
