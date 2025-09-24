using System;

#if NETSTANDARD2_0
using System.Reflection;
#endif

namespace Box.V2.Utility
{
    /// <summary>
    /// Cross platform helpers.
    /// </summary>
    public static class CrossPlatform
    {
        /// <summary>
        /// Check if Type can convert to T
        /// </summary>
        /// <typeparam name="T">Convert to type.</typeparam>
        /// <param name="objectType">Convert from type.</param>
        /// <returns>true if able to convert.</returns>
        public static bool CanConvert<T>(Type objectType)
        {
#if NETSTANDARD2_0
            return typeof(T).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
#else
            return typeof(T).IsAssignableFrom(objectType);
#endif
        }

        /// <summary>
        /// Check if sourceType can convert to targetType
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="sourceType"></param>
        /// <returns>true if able to convert.</returns>
        public static bool CanConvert(Type targetType, Type sourceType)
        {
#if NETSTANDARD2_0
            return targetType.GetTypeInfo().IsAssignableFrom(sourceType.GetTypeInfo());
#else
            return targetType.IsAssignableFrom(sourceType);
#endif
        }
    }
}
