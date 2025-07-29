using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class OAuthConfig {
        public string ClientId { get; }

        public string ClientSecret { get; }

        public ITokenStorage TokenStorage { get; }

        public OAuthConfig(string clientId, string clientSecret, ITokenStorage? tokenStorage = default) {
            ClientId = clientId;
            ClientSecret = clientSecret;
            TokenStorage = tokenStorage ?? new InMemoryTokenStorage();
        }
    }
}