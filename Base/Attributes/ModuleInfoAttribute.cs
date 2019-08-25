//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using System.ComponentModel;

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
