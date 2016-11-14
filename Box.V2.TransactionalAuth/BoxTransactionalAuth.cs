using Box.V2.Auth;
using Box.V2.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Extensions;
using Box.V2.Converter;
using Box.V2.Services;
using Box.V2.Request;
using System.Net;

namespace Box.V2.TransactionalAuth
{
    /// <summary>
    /// Represents an authenticated transactional connection to the Box API
    /// </summary>
    public class BoxTransactionalAuth
    {
        const string TOKEN_TYPE = "bearer";
        private BoxConfig boxConfig;
        /// <summary>
        /// Sessions with the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Authenticated session</returns>
        public OAuthSession Session(string token)
        {
            return new OAuthSession(token, null, 3600, TOKEN_TYPE);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BoxTransactionalAuth"/> class.
        /// </summary>
        public BoxTransactionalAuth()
        {
            this.boxConfig = new BoxConfig("", "", new Uri(Constants.BoxApiHostUriString));
        }
        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="primaryOrSecondaryToken">The primary or secondary token.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="scope">The scope.</param>
        /// <returns>Access token</returns>
        public string GetAccessToken(string primaryOrSecondaryToken, string resource = null, string scope = null)
        {
            if (scope == null)
            {
                scope = Constants.RequestParameters.ScopeDefaultValue;
            }
            var result = TransactionalAuthPost(primaryOrSecondaryToken, scope, resource);
            return result.AccessToken;
        }
        /// <summary>
        /// Transactionals the authentication post.
        /// </summary>
        /// <param name="primaryOrSecondaryToken">The primary or secondary token.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        private OAuthSession TransactionalAuthPost(string primaryOrSecondaryToken, string scope, string resource)
        {
            BoxRequest boxRequest = new BoxRequest(boxConfig.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload(Constants.RequestParameters.SubjectToken, primaryOrSecondaryToken)
                                            .Payload(Constants.RequestParameters.SubjectTokenType, Constants.RequestParameters.SubjectTokenTypeValue)
                                            .Payload(Constants.RequestParameters.Scope, scope)
                                            .Payload(Constants.RequestParameters.Resource, resource)
                                            .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.TransactionalGrantTypeValue);

            var handler = new HttpRequestHandler();
            var converter = new BoxJsonConverter();
            var service = new BoxService(handler);

            IBoxResponse<OAuthSession> boxResponse = service.ToResponseAsync<OAuthSession>(boxRequest).Result;
            boxResponse.ParseResults(converter);

            return boxResponse.ResponseObject;
        }
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <param name="primarOrSecondaryAccessToken">The primar or secondary access token.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="resource">The resource.</param>
        /// <returns>Box client comunicating with box API</returns>
        public BoxClient GetClient(string primarOrSecondaryAccessToken, string resource = null, string scope = null)
        {
            var session = this.Session(this.GetAccessToken(primarOrSecondaryAccessToken, resource, scope));
            var authRepo = new TransactionalAuthRepository(session, this);
            var client = new BoxClient(this.boxConfig, authRepo);
            return client;
        }
    }
}
