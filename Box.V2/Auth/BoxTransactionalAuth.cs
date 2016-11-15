using System;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Extensions;
using Box.V2.Converter;
using Box.V2.Services;
using Box.V2.Request;

namespace Box.V2.TransactionalAuth
{
    /// <summary>
    /// Represents an authenticated transactional connection to the Box API.
    /// </summary>
    public class BoxTransactionalAuth
    {
        const string TOKEN_TYPE = "bearer";
        
        private BoxConfig boxConfig;

        /// <summary>
        /// Sessions with the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Authenticated session.</returns>
        public OAuthSession Session(string token)
        {
            return new OAuthSession(token, null, 3600, TOKEN_TYPE);
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BoxTransactionalAuth"/> class.
        /// </summary>
        public BoxTransactionalAuth()
        {
            this.boxConfig = new BoxConfig(string.Empty, string.Empty, new Uri(Constants.BoxApiHostUriString));
        }

        /// <summary>
        /// Down scope an access token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="scope">The scope to be limited to.</param>
        /// <param name="resource">The resource to be limited to.</param>
        /// <returns>The down scoped access token.</returns>
        public string TokenExchange(string token, string scope, string resource = null)
        {
            if (scope == null)
            {
                scope = Constants.RequestParameters.ScopeDefaultValue;
            }

            BoxRequest boxRequest = new BoxRequest(boxConfig.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                .Method(RequestMethod.Post)
                                .Payload(Constants.RequestParameters.SubjectToken, token)
                                .Payload(Constants.RequestParameters.SubjectTokenType, Constants.RequestParameters.SubjectTokenTypeValue)
                                .Payload(Constants.RequestParameters.Scope, scope)
                                .Payload(Constants.RequestParameters.Resource, resource)
                                .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.TokenExchangeGrantTypeValue);

            var handler = new HttpRequestHandler();
            var converter = new BoxJsonConverter();
            var service = new BoxService(handler);

            IBoxResponse<OAuthSession> boxResponse = service.ToResponseAsync<OAuthSession>(boxRequest).Result;
            boxResponse.ParseResults(converter);

            return boxResponse.ResponseObject.AccessToken;
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="scope">The scope to be limited to.</param>
        /// <param name="resource">The resource to be limited to.</param>
        /// <returns>Box client comunicating with box API.</returns>
        public BoxClient GetClient(string token, string scope, string resource = null)
        {
            var downScopedToken = TokenExchange(token, scope, resource);
            var session = Session(downScopedToken);
            return new BoxClient(boxConfig, session);
        }
    }
}
