//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2018 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using System.Linq;
using System.Reflection;

namespace CodeStack.SwEx.Common.Reflection
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// Tries to get attribute from the assembly
        /// </summary>
        /// <typeparam name="TAtt">Type of attribute to get</typeparam>
        /// <param name="assm">Assembly</param>
        /// <param name="attProc">Action to process attribute</param>
        /// <returns>True if attribute exists</returns>
        public static bool TryGetAttribute<TAtt>(this Assembly assm, Action<TAtt> attProc)
            where TAtt : Attribute
        {
            var atts = assm.GetCustomAttributes(typeof(TAtt), true);

            if (atts != null && atts.Any())
            {
                var att = atts.First() as TAtt;
                attProc?.Invoke(att);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
