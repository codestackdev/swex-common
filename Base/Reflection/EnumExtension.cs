//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2018 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using System.Linq;

namespace CodeStack.SwEx.Common.Reflection
{
    public static class EnumExtension
    {
        /// <summary>
        /// Get the specified attribute from the enumerator field
        /// </summary>
        /// <typeparam name="TAtt">Attribute type</typeparam>
        /// <param name="enumer">Enumerator field</param>
        /// <returns>Attribute</returns>
        /// <exception cref="NullReferenceException"/>
        /// <remarks>This method throws an exception if attribute is missing</remarks>
        public static TAtt GetAttribute<TAtt>(this Enum enumer)
            where TAtt : Attribute
        {
            TAtt att = default(TAtt);

            if (!TryGetAttribute<TAtt>(enumer, a => att = a))
            {
                throw new NullReferenceException($"Attribute of type {typeof(TAtt)} is not fond on {enumer}");
            }

            return att;
        }

        public static bool TryGetAttribute<TAtt>(this Enum enumer, Action<TAtt> attProc)
            where TAtt : Attribute
        {
            var enumType = enumer.GetType();
            var enumField = enumType.GetMember(enumer.ToString()).FirstOrDefault();
            var atts = enumField.GetCustomAttributes(typeof(TAtt), false);

            if (atts != null && atts.Any())
            {
                var att = atts.First() as TAtt;
                attProc.Invoke(att);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}