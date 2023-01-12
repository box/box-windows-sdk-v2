using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Request;
using Box.V2.Services;

namespace Box.V2.CCGAuth
{
    public class BoxCCGAuth
    {
        private readonly IBoxService _boxService;
        private readonly IBoxConfig _boxConfig;

        /// <summary>
        /// Constructor for CCG authentication
        /// </summary>
        /// <param name="boxConfig">Config contains information about client id, client secret, enterprise id.</param>
        /// <param name="boxService">Box service is used to perform GetToken requests</param>
        public BoxCCGAuth(IBoxConfig boxConfig, IBoxService boxService)
        {
            _boxConfig = boxConfig;
            _boxService = boxService;
        }

        /// <summary>
        /// Constructor for CCG authentication with default boxService
        /// </summary>
        /// <param name="boxConfig">Config contains information about client id, client secret, enterprise id.</param>
        public BoxCCGAuth(IBoxConfig boxConfig) : this(boxConfig, new BoxService(new HttpRequestHandler(boxConfig.WebProxy, boxConfig.Timeout)))
        {

        }

        /// <summary>
        /// Create admin BoxClient using an admin access token
        /// </summary>
        /// <param name="adminToken">Admin access token</param>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        /// <returns>BoxClient that uses CCG authentication</returns>
        public IBoxClient AdminClient(string adminToken, string asUser = null, bool? suppressNotifications = null)
        {
            var adminSession = Session(adminToken);
            var authRepo = new CCGAuthRepository(adminSession, this);
            var adminClient = new BoxClient(_boxConfig, authRepo, asUser: asUser, suppressNotifications: suppressNotifications);

            return adminClient;
        }

        /// <summary>
        /// Create admin BoxClient
        /// </summary>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        /// <returns>BoxClient that uses CCG authentication</returns>
        public IBoxClient AdminClient(string asUser = null, bool? suppressNotifications = null)
        {
            var authRepo = new CCGAuthRepository(this);
            var adminClient = new BoxClient(_boxConfig, authRepo, asUser: asUser, suppressNotifications: suppressNotifications);

            return adminClient;
        }

        /// <summary>
        /// Create user BoxClient using a user access token
        /// </summary>
        /// <param name="userToken">User access token</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>BoxClient that uses CCG authentication</returns>
        public IBoxClient UserClient(string userToken, string userId)
        {
            var userSession = Session(userToken);
            var authRepo = new CCGAuthRepository(userSession, this, userId);
            var userClient = new BoxClient(_boxConfig, authRepo);

            return userClient;
        }

        /// <summary>
        /// Create user BoxClient
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>BoxClient that uses CCG authentication</returns>
        public IBoxClient UserClient(string userId)
        {
            var authRepo = new CCGAuthRepository(this, userId);
            var userClient = new BoxClient(_boxConfig, authRepo);

            return userClient;
        }

        /// <summary>
        /// Get admin token by posting data to auth url
        /// </summary>
        /// <returns>Admin token</returns>
        public async Task<string> AdminTokenAsync()
        {
            return (await CCGAuthPostAsync(Constants.RequestParameters.EnterpriseSubType, _boxConfig.EnterpriseId).ConfigureAwait(false)).AccessToken;
        }

        /// <summary>
        /// Once you have created an App User or Managed User, you can request a User Access Token via the App Auth feature, which will return the OAuth 2.0 access token for the specified User.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>User token</returns>
        public async Task<string> UserTokenAsync(string userId)
        {
            return (await CCGAuthPostAsync(Constants.RequestParameters.UserSubType, userId).ConfigureAwait(false)).AccessToken;
        }

        private async Task<OAuthSession> CCGAuthPostAsync(string subType, string subId)
        {
            BoxRequest boxRequest = new BoxRequest(_boxConfig.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.ClientCredentials)
                                            .Payload(Constants.RequestParameters.ClientId, _boxConfig.ClientId)
                                            .Payload(Constants.RequestParameters.ClientSecret, _boxConfig.ClientSecret)
                                            .Payload(Constants.RequestParameters.SubjectType, subType)
                                            .Payload(Constants.RequestParameters.SubjectId, subId);

            var converter = new BoxJsonConverter();
            IBoxResponse<OAuthSession> boxResponse = await _boxService.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
            boxResponse.ParseResults(converter);

            return boxResponse.ResponseObject;

        }

        /// <summary>
        /// Create OAuth session from token
        /// </summary>
        /// <param name="token">Access token created by method UserToken, or AdminToken</param>
        /// <returns>OAuth session</returns>
        public OAuthSession Session(string token)
        {
            return new OAuthSession(token, null, Constants.AccessTokenExpirationTime, Constants.BearerTokenType);
        }
    }
}
