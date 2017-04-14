using System.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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

        public static string GetSha1Hash(Stream stream)
        {
#if NETSTANDARD1_4
            stream.Position = 0;
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(stream);
            string base64String = Convert.ToBase64String(hash);
            return base64String;
#else
            /*
            var key = new byte[32];
            var contentBytes = Encoding.UTF8.GetBytes("some kind of content to hash");
            new RNGCryptoServiceProvider().GetBytes(key);

            var alg = new HMACSHA1(key); // Bouncy castle usage does not differ from this
            var result = alg.ComputeHash(contentBytes);
            */

            // TODO yhu@ sha1 in pcl
            var keyBytes = new byte[32];
            HMACSHA1 hashAlgorithm = new HMACSHA1(keyBytes);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(stream.ToString());
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
#endif
        }
    }
}