using System;

namespace Box.Sdk.Gen
{
    /// <summary>
    /// Default exponential backoff strategy used by the HTTP Client.
    /// </summary>
    public class ExponentialBackoffRetryStrategy : IRetryStrategy
    {
        /// <summary>
        /// Returns the time interval after which to retry the request.
        /// </summary>
        /// <param name="attemptNumber">Attempt number</param>
        /// <returns>Retry interval in seconds</returns>
        public int GetRetryTimeout(int attemptNumber)
        {
            var baseInterval = TimeSpan.FromSeconds(1.0);
            const double RETRY_RANDOMIZATION_FACTOR = 0.5;
            var minRandomization = 1 - RETRY_RANDOMIZATION_FACTOR;
            var maxRandomization = 1 + RETRY_RANDOMIZATION_FACTOR;
            var random = new Random();

            var randomization = random.NextDouble() * (maxRandomization - minRandomization) + minRandomization;
            var exponential = Math.Pow(2, attemptNumber);
            return (int)Math.Ceiling(exponential * baseInterval.TotalSeconds * randomization);
        }
    }
}