using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class DeveloperTokenConfig {
        public string? ClientId { get; init; }

        public string? ClientSecret { get; init; }

        public DeveloperTokenConfig() {
            
        }
    }
}