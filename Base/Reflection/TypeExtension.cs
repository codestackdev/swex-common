﻿//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace CodeStack.SwEx.Common.Reflection
{
    /// <summary>
    /// Provides the extension methods for <see cref="Type"/>
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Get the specified attribute from the type, all parent types and interfaces
        /// </summary>
        /// <typeparam name="TAtt">Attribute type</typeparam>
        /// <param name="type">Type</param>
        /// <returns>Attribute</returns>
        /// <exception cref="NullReferenceException"/>
        /// <remarks>This method throws an exception if attribute is missing</remarks>
        public static TAtt GetAttribute<TAtt>(this Type type)
                    where TAtt : Attribute
        {
            TAtt att = default(TAtt);

            if (!TryGetAttribute<TAtt>(type, a => att = a))
            {
                throw new NullReferenceException($"Attribute of type {typeof(TAtt)} is not fond on {type.FullName}");
            }

            return att;
        }

        /// <summary>
        /// Attempts to the attribute from type, all parent types and interfaces
        /// </summary>
        /// <typeparam name="TAtt">Type of the attribute</typeparam>
        /// <param name="type">Type to get attribute from</param>
        /// <returns>Attribute or null if not found</returns>
        public static TAtt TryGetAttribute<TAtt>(this Type type)
            where TAtt : Attribute
        {
            TAtt thisAtt = null;
            TryGetAttribute<TAtt>(type, a => thisAtt = a);
            return thisAtt;
        }

        /// <summary>
        /// Attempts to get the attribute from the type, all parent types and interfaces
        /// </summary>
        /// <typeparam name="TAtt">Type of the attribute</typeparam>
        /// <param name="type">Type to get attribute from</param>
        /// <param name="att">Attribute of the type</param>
        /// <returns>True if attribute exists</returns>
        public static bool TryGetAttribute<TAtt>(this Type type, out TAtt att)
            where TAtt : Attribute
        {
            TAtt thisAtt = null;
            var res = TryGetAttribute<TAtt>(type, a => thisAtt = a);
            att = thisAtt;
            return res;
        }

        /// <summary>
        /// Attempts to get the attribute from the type, all parent types and interfaces
        /// </summary>
        /// <typeparam name="TAtt">Type of the attribute</typeparam>
        /// <param name="type">Type to get attribute from</param>
        /// <param name="attProc">Handler to process the attribute</param>
        /// <returns>True if attribute exists</returns>
        public static bool TryGetAttribute<TAtt>(this Type type, Action<TAtt> attProc)
            where TAtt : Attribute
        {
            var atts = type.GetCustomAttributes(typeof(TAtt), true).
                Union(type.GetInterfaces().
                SelectMany(interfaceType => interfaceType.GetCustomAttributes(typeof(TAtt), true))).
                Distinct();

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

        /// <summary>
        /// Checks if this type can be assigned to the generic type
        /// </summary>
        /// <param name="thisType">Type</param>
        /// <param name="genericType">Base generic type (i.e. MyGenericType&lt;&gt;)</param>
        /// <returns>True if type is assignable to generic</returns>
        public static bool IsAssignableToGenericType(this Type thisType, Type genericType)
        {
            return thisType.TryFindGenericType(genericType) != null;
        }

        /// <summary>
        /// Gets the specific arguments of this type in relation to specified generic type
        /// </summary>
        /// <param name="thisType">This type which must be assignable to the specified genericType</param>
        /// <param name="genericType">Generic type</param>
        /// <returns>Arguments</returns>
        /// <remarks>For example this method called on List&lt;string&gt; where the genericType is IEnumerable&lt;&gt; would return string</remarks>
        public static Type[] GetArgumentsOfGenericType(this Type thisType, Type genericType)
        {
            var type = thisType.TryFindGenericType(genericType);

            if (type != null)
            {
                return type.GetGenericArguments();
            }
            else
            {
                return Type.EmptyTypes;
            }
        }

        /// <summary>
        /// Finds the specific generic type to a specified base generic type
        /// </summary>
        /// <param name="thisType">This type</param>
        /// <param name="genericType">Base generic type</param>
        /// <returns>Specific generic type or null if not found</returns>
        public static Type TryFindGenericType(this Type thisType, Type genericType)
        {
            var interfaceTypes = thisType.GetInterfaces();

            Predicate<Type> canCastFunc = (t) => t.IsGenericType && t.GetGenericTypeDefinition() == genericType;

            foreach (var it in interfaceTypes)
            {
                if (canCastFunc(it))
                {
                    return it;
                }
            }

            if (canCastFunc(thisType))
            {
                return thisType;
            }

            var baseType = thisType.BaseType;

            if (baseType != null)
            {
                return baseType.TryFindGenericType(genericType);
            }

            return null;
        }

        /// <summary>
        /// Returns the COM ProgId of a type
        /// </summary>
        /// <param name="type">Input type</param>
        /// <returns>COM Prog id</returns>
        public static string GetProgId(this Type type)
        {
            string progId = "";

            if (!type.TryGetAttribute<ProgIdAttribute>(a => progId = a.Value))
            {
                progId = type.FullName;
            }

            return progId;
        }

        /// <summary>
        /// Identifies if type is COM visible
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if type is COM visible</returns>
        public static bool IsComVisible(this Type type)
        {
            bool isComVisible = false;

            var comVisAtt = Attribute.GetCustomAttribute(type, typeof(ComVisibleAttribute), false) as ComVisibleAttribute;

            if (comVisAtt != null)
            {
                isComVisible = comVisAtt.Value;
            }
            else
            {
                type.Assembly.TryGetAttribute<ComVisibleAttribute>(a => isComVisible = a.Value);
            }
            

            return isComVisible;
        }
    }
}