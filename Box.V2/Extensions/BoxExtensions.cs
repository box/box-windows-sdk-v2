using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Extensions
{
    internal static class BoxExtensions
    {
        /// <summary>
        /// Checks if the object is null 
        /// </summary>
        /// <typeparam name="T">Type of the object being checked</typeparam>
        /// <param name="param"></param>
        /// <param name="name"></param>
        internal static T ThrowIfNull<T>(this T param, string name) 
        {
            if (param == null)
                throw new ArgumentNullException(name);

            return param;
        }

        /// <summary>
        /// Checks if a string is null or whitespace
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string ThrowIfNullOrWhiteSpace(this string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Required field cannot be null or whitespace", name);

            return value;
        }
    }
}
