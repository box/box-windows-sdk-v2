using System;
using Box.V2.Utility;

namespace Box.V2.Test.Helpers
{
    public class InstantRetryStrategy : IRetryStrategy
    {
        TimeSpan IRetryStrategy.GetRetryTimeout(int numRetries)
        {
            return TimeSpan.Zero;
        }
    }
}
