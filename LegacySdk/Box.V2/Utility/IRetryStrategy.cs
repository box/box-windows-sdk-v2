using System;

namespace Box.V2.Utility
{
    /// <summary>
    /// Retry strategy used when retrying HTTP request.
    /// </summary>
    public interface IRetryStrategy
    {
        /// <summary>
        /// Returns the time interval after which to retry the request.
        /// </summary>
        /// <param name="numRetries">Retry number</param>
        /// <returns>Retry interval</returns>
        TimeSpan GetRetryTimeout(int numRetries);
    }
}
