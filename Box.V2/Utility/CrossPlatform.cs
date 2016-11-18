using System.Threading.Tasks;
using System;

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