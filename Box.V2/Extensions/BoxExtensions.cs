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
        /// <param name="param">Object being checked</param>
        /// <param name="name">Object name</param>
		/// <returns>Param object if valid</returns>
        internal static T ThrowIfNull<T>(this T param, string name)
        {
            if (param == null)
            {
                throw new ArgumentNullException(name);
            }

            return param;
        }

        /// <summary>
        /// Checks if a string is null or whitespace
        /// </summary>
        /// <param name="value">String being checked</param>
        /// <param name="name">String object name</param>
        /// <returns>Input value if valid</returns>
        internal static string ThrowIfNullOrWhiteSpace(this string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Required field cannot be null or whitespace", name);
            }

            return value;
        }

        /// <summary>
        /// Checks if a number is higher than limit
        /// </summary>
        /// <param name="value">Number being checked</param>
        /// <param name="limit">Number maximum limit value</param>
        /// <param name="name">Number object name</param>
        /// <returns>Input value if valid</returns>
        internal static int ThrowIfHigherThan(this int value, int limit, string name)
        {
            if (value > limit)
            {
                throw new ArgumentException(string.Format("Required field cannot be higher than {0}", name, limit));
            }

            return value;
        }
    }
}
