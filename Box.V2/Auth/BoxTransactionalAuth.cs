using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Extensions;
using Box.V2.Converter;
using Box.V2.Services;
using Box.V2.Request;
using System;
using Box.V2.Utility;

namespace Box.V2.TransactionalAuth
{
    /// <summary>
    /// Represents an authenticated transactional connection to the Box API.
    /// </summary>
    public class BoxTransactionalAuth
    {
        const string TOKEN_TYPE = "bearer";
        const string ACTOR_HEADER = "{\"alg\":\"none\"}";
        const string ACTOR_CLAIM_TEMPLATE = "{{\"iss\":\"{0}\",\"sub\":\"{1}\",\"box_sub_type\":\"external\",\"aud\":\"https://api.box.com/oauth2/token\",\"exp\":{2},\"jti\":\"{3}\",\"name\":\"{4}\"}}";
        
        private BoxConfig boxConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxTransactionalAuth"/> class.
        /// </summary>
        public BoxTransactionalAuth(BoxConfig boxConfig)
        {
            this.boxConfig = boxConfig;
        }

        /// <summary>
        /// Get a down scoped token.
        /// </summary>
        /// <param name="scope">The scope to be limited to.</param>
        /// <param name="resource">The resource to be limited to.</param>
        /// <param name="actorToken">The actor user token.</param>
        /// <returns>The down scoped access token.</returns>
        public string TokenExchange(string scope, string resource = null, string actorToken = null)
        {
            BoxRequest boxRequest = new BoxRequest(boxConfig.BoxApiHostUri, Constants.AuthTokenEndpointString)
                .Method(RequestMethod.Post)
                .Payload(Constants.RequestParameters.SubjectToken, boxConfig.Token)
                .Payload(Constants.RequestParameters.SubjectTokenType, Constants.RequestParameters.AccessTokenTypeValue)
                .Payload(Constants.RequestParameters.Scope, scope)
                .Payload(Constants.RequestParameters.Resource, resource)
                .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.TokenExchangeGrantTypeValue);

            if (actorToken != null)
            {
                boxRequest = boxRequest.Payload(Constants.RequestParameters.ActorToken, actorToken)
                    .Payload(Constants.RequestParameters.ActorTokenType, Constants.RequestParameters.IdTokenTypeValue);
            }

            var handler = new HttpRequestHandler();
            var converter = new BoxJsonConverter();
            var service = new BoxService(handler);

            IBoxResponse<OAuthSession> boxResponse = service.ToResponseAsync<OAuthSession>(boxRequest).Result;
            boxResponse.ParseResults(converter);

            return boxResponse.ResponseObject.AccessToken;
        }


        /// <summary>
        /// Construct an actor token.
        /// </summary>
        /// <param name="userId">The external user id.</param>
        /// <param name="userName">The external display name.</param>
        /// <param name="clientId">The clientId.</param>
        /// <returns></returns>
        public static string ConstructActorToken(string userId, string userName, string clientId)
        {
            var exp = Helper.ConvertToUnixTimestamp(DateTime.UtcNow.AddSeconds(30));
            var jti = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var header = Helper.Base64Encode(ACTOR_HEADER);

            var claims = string.Format(ACTOR_CLAIM_TEMPLATE, clientId, userId, exp, jti, userName);
            claims = Helper.Base64Encode(claims);

            return header + "." + claims + ".";
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <returns>Box client comunicating with box API.</returns>
        public BoxClient GetClient()
        {
            var session = new OAuthSession(boxConfig.Token, null, 3600, TOKEN_TYPE);
            return new BoxClient(boxConfig, session);
        }
    }
}
