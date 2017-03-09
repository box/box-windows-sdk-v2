using System.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;

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

        /// <summary>
        /// Returns the message digest of the stream, formatted as specified by RFC 3230.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetSha1Hash(Stream stream)
        {
            stream.Position = 0;
#if NETSTANDARD1_4
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(stream);
            string base64String = Convert.ToBase64String(hash);
            return base64String;
#else
            throw new NotImplementedException("SHA1 hash function is not implemented in Portable library");
#endif
        }
    }
}