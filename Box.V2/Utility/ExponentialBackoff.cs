using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Utility
{
    public class ExponentialBackoff
    {
        public TimeSpan GetRetryTimeout(int numRetries, TimeSpan baseInterval)
        {
            const double RETRY_RANDOMIZATION_FACTOR = 0.5;
            var minRandomization = Convert.ToInt32(1 - RETRY_RANDOMIZATION_FACTOR);
            var maxRandomization = Convert.ToInt32(1 - RETRY_RANDOMIZATION_FACTOR);
            Random random = new Random();

            var randomization = random.Next(minRandomization, maxRandomization) + minRandomization;
            var exponential = Math.Pow(2, numRetries - 1);
            return TimeSpan.FromMilliseconds(Math.Ceiling(exponential * Convert.ToInt32(baseInterval) * randomization));
        }
    }
}
