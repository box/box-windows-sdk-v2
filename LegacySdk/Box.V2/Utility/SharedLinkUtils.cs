using System;

namespace Box.V2.Utility
{
    internal static class SharedLinkUtils
    {
        internal static Tuple<string, string> GetSharedLinkHeader(string sharedLink, string sharedLinkPassword)
            => Tuple.Create("BoxApi", string.Format("shared_link={0}{1}", sharedLink, string.IsNullOrEmpty(sharedLinkPassword) ? "" : ("&shared_link_password=" + sharedLinkPassword)));
    }
}
