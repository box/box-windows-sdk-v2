using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class OAuthConfig {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public ITokenStorage TokenStorage { get; set; }

        public OAuthConfig(string clientId, string clientSecret, ITokenStorage tokenStorage = default) {
            ClientId = clientId;
            ClientSecret = clientSecret;
            TokenStorage = tokenStorage ?? new InMemoryTokenStorage();
        }
    }
}