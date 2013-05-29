using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.BoxWrappers
{
    public static class BoxResponseExtensions
    {
        public static IBoxResponse<T> RetryOnceOnExpiredToken<T>(this IBoxResponse<T> response, IBoxRequest request)
        {
            return response;
        }
    }
}
