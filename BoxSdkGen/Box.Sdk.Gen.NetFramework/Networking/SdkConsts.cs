using System.Collections.Generic;

namespace Box.Sdk.Gen.Internal
{
    static class SdkConsts
    {
        internal static string RunTimeVersion = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

        internal static KeyValuePair<string, string> UserAgentHeader = new KeyValuePair<string, string>("User-Agent", $"box-dotnet-generated-sdk/{Version.sdkVersion}");
        internal static KeyValuePair<string, string> BoxUAHeader = new KeyValuePair<string, string>("X-Box-UA", $"agent={UserAgentHeader.Value}; env=dotnet/{RunTimeVersion}");

        internal static List<KeyValuePair<string, string>> AnalyticsHeaders = new List<KeyValuePair<string, string>>() { UserAgentHeader, BoxUAHeader };
    }
}
