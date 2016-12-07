using System;

namespace Box.V2.Utility
{
    /// <summary>
    /// A helper class.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Convert DateTime to unix timestamp.
        /// </summary>
        /// <param name="date">DateTime object.</param>
        /// <returns>unix timestamp.</returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Encode string to base64
        /// </summary>
        /// <param name="plainText"> the string to be encoded.</param>
        /// <returns>base64 encoded string.</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}