using System.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

#if NETSTANDARD1_4
using System.Reflection;
#endif

namespace Box.V2.Utility
{
    /// <summary>
    /// Cross platform helpers.
    /// </summary>
    public static class CrossPlatform
    {
        public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
        {
#if NETSTANDARD1_4
            return Task.WhenAll(tasks);
#else
            return TaskEx.WhenAll(tasks);
#endif
        }

        /// <summary>
        /// Starts a Task that will complete after the specified due time.
        /// </summary>
        /// <param name="dueTime">The delay before the returned task completes.</param>
        /// <returns>The timed Task.</returns>
        public static Task Delay(int dueTime)
        {
#if NETSTANDARD1_4
            return Task.Delay(dueTime);
#else
            return TaskEx.Delay(dueTime);
#endif
        }

        /// <summary>
        /// Check if Type can convert to T
        /// </summary>
        /// <typeparam name="T">Convert to type.</typeparam>
        /// <param name="objectType">Convert from type.</param>
        /// <returns>true if able to convert.</returns>
        public static bool CanConvert<T>(Type objectType)
        {
#if NETSTANDARD1_4
            return typeof(T).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
#else
            return typeof(T).IsAssignableFrom(objectType);
#endif
        }
    }
}