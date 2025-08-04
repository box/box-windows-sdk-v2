using Box.Sdk.Gen;

namespace Box.Sdk.Gen {
    public class ProxyConfig {
        public string Url { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public ProxyConfig(string url, bool useDefaultCredentials = false) {
            Url = url;
            UseDefaultCredentials = useDefaultCredentials;
        }
    }
}