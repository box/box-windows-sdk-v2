using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class CcgConfig {
        /// <summary>
        /// Box API key used for identifying the application the user is authenticating with
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Box API secret used for making auth requests.
        /// </summary>
        public string ClientSecret { get; }

        /// <summary>
        /// The ID of the Box Developer Edition enterprise.
        /// </summary>
        public string? EnterpriseId { get; init; }

        /// <summary>
        /// The user id to authenticate. This value is not required. But if it is provided, then the user will be auto-authenticated at the time of the first API call.
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// Object responsible for storing token. If no custom implementation provided,the token will be stored in memory.
        /// </summary>
        public ITokenStorage TokenStorage { get; }

        public CcgConfig(string clientId, string clientSecret, ITokenStorage? tokenStorage = default) {
            ClientId = clientId;
            ClientSecret = clientSecret;
            TokenStorage = tokenStorage ?? new InMemoryTokenStorage();
        }
    }
}