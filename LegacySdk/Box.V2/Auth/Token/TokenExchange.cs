using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Request;
using Box.V2.Services;

namespace Box.V2.Auth.Token
{
    /// <summary>
    /// Logic for token exchange.
    /// </summary>
    public class TokenExchange
    {
        // required fields
        protected string token;
        protected List<string> scopes = new List<string>();

        // optional fields
        protected string actorToken;
        protected string resourceUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenExchange"/> class.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="scope">The scope to be limited to.</param>
        public TokenExchange(string token, string scope)
        {
            this.token = token;
            scopes.Add(scope);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenExchange"/> class.
        /// </summary>
        /// <param name="token">The access token.</param>
        /// <param name="scopes">The scopes to be limited to.</param>
        public TokenExchange(string token, IEnumerable<string> scopes)
        {
            this.token = token;
            this.scopes.AddRange(scopes);
        }

        /// <summary>
        /// Set actor token.
        /// </summary>
        /// <param name="actorToken">The actor user token.</param>
        public void SetActorToken(string actorToken)
        {
            this.actorToken = actorToken;
        }

        /// <summary>
        /// Set resource.
        /// </summary>
        /// <param name="resourceUrl">The resource url.</param>
        public void SetResource(string resourceUrl)
        {
            this.resourceUrl = resourceUrl;
        }

        /// <summary>
        /// Get a down scoped token.
        /// </summary>
        /// <returns>The down scoped access token.</returns>
        public async Task<string> ExchangeAsync()
        {
            BoxRequest boxRequest = new BoxRequest(new Uri(Constants.BoxApiHostUriString), Constants.AuthTokenEndpointString)
                .Method(RequestMethod.Post)
                .Payload(Constants.RequestParameters.SubjectToken, token)
                .Payload(Constants.RequestParameters.SubjectTokenType, Constants.RequestParameters.AccessTokenTypeValue)
                .Payload(Constants.RequestParameters.Scope, string.Join(" ", scopes))
                .Payload(Constants.RequestParameters.Resource, resourceUrl)
                .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.TokenExchangeGrantTypeValue);

            if (actorToken != null)
            {
                boxRequest = boxRequest.Payload(Constants.RequestParameters.ActorToken, actorToken)
                    .Payload(Constants.RequestParameters.ActorTokenType, Constants.RequestParameters.IdTokenTypeValue);
            }

            var handler = new HttpRequestHandler();
            var converter = new BoxJsonConverter();
            var service = new BoxService(handler);

            IBoxResponse<OAuthSession> boxResponse = await service.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
            boxResponse.ParseResults(converter);

            return boxResponse.ResponseObject.AccessToken;
        }
    }
}
