using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class GetAuthorizeUrlOptions {
        /// <summary>
        /// Box API key used for identifying the application the user is authenticating with
        /// </summary>
        public string? ClientId { get; init; }

        /// <summary>
        /// The URI to which Box redirects the browser after the user has granted or denied the application permission. This URI match one of the redirect URIs in the configuration of your application.
        /// </summary>
        public string? RedirectUri { get; init; }

        /// <summary>
        /// The type of response we would like to receive.
        /// </summary>
        public string? ResponseType { get; init; }

        /// <summary>
        /// A custom string of your choice. Box will pass the same string to the redirect URL when authentication is complete. This parameter can be used to identify a user on redirect, as well as protect against hijacked sessions and other exploits.
        /// </summary>
        public string? State { get; init; }

        /// <summary>
        /// A space-separated list of application scopes you'd like to authenticate the user for. This defaults to all the scopes configured for the application in its configuration page.
        /// </summary>
        public string? Scope { get; init; }

        public GetAuthorizeUrlOptions() {
            
        }
    }
}