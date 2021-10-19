using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Box.V2.Config;

namespace Box.V2.Utility
{
    /// <summary>
    /// A helper class.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Convert DateTimeOffset to unix timestamp.
        /// </summary>
        /// <param name="date">DateTimeOffset object.</param>
        /// <returns>unix timestamp.</returns>
        public static double ConvertToUnixTimestamp(DateTimeOffset date)
        {
            var origin = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Convert DateTime to RFC3339 string.
        /// </summary>
        /// <param name="date">DateTime object.</param>
        /// <returns>RFC3339 string.</returns>
        public static string ConvertToRFCString(DateTime? date)
        {
            return date.HasValue ? date.Value.ToString(Constants.RFC3339DateFormat) : null;
        }

        /// <summary>
        /// Convert DateTimeOffset to RFC3339 string.
        /// </summary>
        /// <param name="date">DateTime object.</param>
        /// <returns>RFC3339 string.</returns>
        public static string ConvertToRFCString(DateTimeOffset? date)
        {
            return date.HasValue ? date.Value.ToString(Constants.RFC3339DateFormat) : null;
        }

        /// <summary>
        /// Encode string to base64
        /// </summary>
        /// <param name="plainText"> the string to be encoded.</param>
        /// <returns>base64 encoded string.</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Parses a URL and returns query string
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseQueryString(Uri uri)
        {
            var query = uri.Query.Substring(uri.Query.IndexOf('?') + 1); // +1 for skipping '?'
            var pairs = query.Split('&');
            return pairs
                .Select(o => o.Split('='))
                .Where(items => items.Count() == 2)
                .ToDictionary(pair => Uri.UnescapeDataString(pair[0]),
                    pair => Uri.UnescapeDataString(pair[1]));
        }

        /// <summary>
        /// Calculate sha1 hash.
        /// </summary>
        /// <param name="stream"> the input stream. </param>
        /// <returns>Base64 encoded sha1 hash.</returns>
        public static string GetSha1Hash(Stream stream)
        {
            stream.Position = 0;

            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(stream);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
