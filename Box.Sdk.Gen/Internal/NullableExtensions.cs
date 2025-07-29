using System;

namespace Box.Sdk.Gen.Internal 
{
    public class NullableUtils
    {
        public static T Unwrap<T>(T? obj) where T : class
        {
            if (obj == null)
            {
                throw new InvalidOperationException("Nullable object must have a value.");
            }
            return obj;
        }

        public static T Unwrap<T>(T? val) where T : struct => val!.Value;

    }
}
