using System;

namespace Box.V2.Utility
{
    public class ExponentialBackoff : IRetryStrategy
    {
        public TimeSpan GetRetryTimeout(int numRetries)
        {
            var baseInterval = TimeSpan.FromSeconds(2.0);
            const double RETRY_RANDOMIZATION_FACTOR = 0.5;
            var minRandomization = 1 - RETRY_RANDOMIZATION_FACTOR;
            var maxRandomization = 1 + RETRY_RANDOMIZATION_FACTOR;
            var random = new Random();

            var randomization = random.NextDouble() * (maxRandomization - minRandomization) + minRandomization;
            var exponential = Math.Pow(2, numRetries - 1);
            var result = Math.Ceiling(exponential * baseInterval.TotalSeconds * randomization);
            return TimeSpan.FromSeconds(result);
        }
    }
}
