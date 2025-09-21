using Box.Sdk.Gen;

namespace Box.Sdk.Gen {
    public class ProxyConfig {
        public string Url { get; }

        public string? Username { get; init; }

        public string? Password { get; init; }

        public string? Domain { get; init; }

        public bool UseDefaultCredentials { get; }

        public ProxyConfig(string url, bool useDefaultCredentials = false) {
            Url = url;
            UseDefaultCredentials = useDefaultCredentials;
        }
    }
}