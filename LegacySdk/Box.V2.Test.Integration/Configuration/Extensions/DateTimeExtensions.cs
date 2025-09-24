using System;

namespace Box.V2.Test.Integration.Configuration.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsEqualUpToSeconds(this DateTimeOffset dt1, DateTimeOffset dt2)
        {
            return dt1.Year == dt2.Year && dt1.Month == dt2.Month && dt1.Day == dt2.Day &&
                   dt1.Hour == dt2.Hour && dt1.Minute == dt2.Minute && dt1.Second == dt2.Second;
        }
    }
}
