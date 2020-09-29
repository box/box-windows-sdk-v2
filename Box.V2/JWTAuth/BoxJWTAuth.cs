using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Extensions;
using Box.V2.Request;
using Box.V2.Services;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using Box.V2.Utility;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace Box.V2.JWTAuth
{
    ///<summary>
    /// Box’s new authentication model allows applications to authenticate directly to Box using a JSON Web Token (JWT) signed with an RSA key. This authentication method is meant for server-to-server applications and replaces the first leg of the standard 3-legged OAuth 2.0 process in which users grant an application authorization to access their Box account.
    ///</summary>
    ///<remarks>
    /// https://developer.box.com/en/guides/applications/custom-apps/
    ///</remarks>
    public class BoxJWTAuth
    {
        const string AUTH_URL = "https://api.box.com/oauth2/token";
        const string ENTERPRISE_SUB_TYPE = "enterprise";
        const string USER_SUB_TYPE = "user";
        const string TOKEN_TYPE = "bearer";

        readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1);

        private readonly IBoxService boxService;
        private readonly IBoxConfig boxConfig;
        private readonly SigningCredentials credentials;

        /// <summary>
        /// Constructor for JWT authentication
        /// </summary>
        /// <param name="boxConfig">Config contains information about client id, client secret, enterprise id, private key, private key password, public key id </param>
        /// <param name="boxService">Box service is used to perform GetToken requests</param>
        public BoxJWTAuth(IBoxConfig boxConfig, IBoxService boxService)
        {
            this.boxConfig = boxConfig;
            this.boxService = boxService;

            // the following allows creation of a BoxJWTAuth object without valid keys but with a valid JWT UserToken
            // this allows code like this:

            // var boxConfig = new BoxConfig("", "", "", "", "", "");
            // var boxJwt = new BoxJWTAuth(boxConfig);
            // const string userToken = "TOKEN_OBTAINED_BY_CALLING_FULL_BOXJWTAUTH";  // token valid for 1 hr.
            // UserClient = boxJwt.UserClient(userToken, null);  // this user client can do normal file operations.

            if (!string.IsNullOrEmpty(boxConfig.JWTPrivateKey) && !string.IsNullOrEmpty(boxConfig.JWTPrivateKeyPassword))
            {
                var pwf = new PEMPasswordFinder(this.boxConfig.JWTPrivateKeyPassword);
                object key = null;
                using (var reader = new StringReader(this.boxConfig.JWTPrivateKey))
                {
                    var privateKey = new PemReader(reader, pwf).ReadObject();

                    key = privateKey;
                }

                if (key == null)
                {
                    throw new BoxException("Invalid private key!");
                }

                RSA rsa = null;
                if (key is AsymmetricCipherKeyPair)
                {
                    var ackp = (AsymmetricCipherKeyPair)key;
                    rsa = RSAUtilities.ToRSA((RsaPrivateCrtKeyParameters)ackp.Private);
                }
                else if (key is RsaPrivateCrtKeyParameters)
                {
                    var rpcp = (RsaPrivateCrtKeyParameters)key;
                    rsa = RSAUtilities.ToRSA(rpcp);
                }

                credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
            }
        }

        /// <summary>
        /// Constructor for JWT authentication with default boxService
        /// </summary>
        /// <param name="boxConfig">Config contains information about client id, client secret, enterprise id, private key, private key password, public key id </param>
        public BoxJWTAuth(IBoxConfig boxConfig) : this(boxConfig, new BoxService(new HttpRequestHandler(boxConfig.WebProxy)))
        {
            
        }

        /// <summary>
        /// Create admin BoxClient using an admin access token
        /// </summary>
        /// <param name="adminToken">Admin access token</param>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        /// <returns>BoxClient that uses JWT authentication</returns>
        public BoxClient AdminClient(string adminToken, string asUser = null, bool? suppressNotifications = null)
        {
            var adminSession = this.Session(adminToken);
            var authRepo = new JWTAuthRepository(adminSession, this);
            var adminClient = new BoxClient(this.boxConfig, authRepo, asUser: asUser, suppressNotifications: suppressNotifications);

            return adminClient;
        }
        /// <summary>
        /// Create user BoxClient using a user access token
        /// </summary>
        /// <param name="userToken">User access token</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>BoxClient that uses JWT authentication</returns>
        public BoxClient UserClient(string userToken, string userId)
        {
            var userSession = this.Session(userToken);
            var authRepo = new JWTAuthRepository(userSession, this, userId);
            var userClient = new BoxClient(this.boxConfig, authRepo);

            return userClient;
        }
        /// <summary>
        /// Get admin token by posting data to auth url
        /// </summary>
        /// <returns>Admin token</returns>
        public string AdminToken()
        {
            return this.GetToken(ENTERPRISE_SUB_TYPE, this.boxConfig.EnterpriseId);
        }
        /// <summary>
        /// Once you have created an App User, you can request a User Access Token via the App Auth feature, which will return the OAuth 2.0 access token for the specified App User.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>User token</returns>
        public string UserToken(string userId)
        {
            return this.GetToken(USER_SUB_TYPE, userId);
        }

        private string GetToken(string subType, string subId)
        {
            int retryCounter = 0;
            ExponentialBackoff expBackoff = new ExponentialBackoff();

            var assertion = ConstructJWTAssertion(subId, subType);
            OAuthSession result;

            while (true)
            {
                try
                {
                    result = JWTAuthPost(assertion);
                    return result.AccessToken;
                }
                catch (BoxException ex)
                {
                    //need to wait for Retry-After seconds and then retry request
                    var retryAfterHeader = ex.ResponseHeaders != null ? ex.ResponseHeaders.RetryAfter : null;

                    // If we get a retryable/transient error code and this is not a multi part request (meaning a file upload, which cannot be retried
                    // because the stream cannot be reset) and we haven't exceeded the number of allowed retries, then retry the request.
                    // If we get a 202 code and has a retry-after header, we will retry after.
                    // If we get a 400 due to exp claim issue, this can happen if the current system time is too different from the Box server time, so retry.
                    var errorCode = ex.Error?.Code ?? ex.Error?.Name;
                    var errorDescription = ex.Error?.Message ?? ex.Error?.Description;

                    if ((ex.StatusCode == HttpRequestHandler.TooManyRequests
                        ||
                        ex.StatusCode == HttpStatusCode.InternalServerError
                        ||
                        ex.StatusCode == HttpStatusCode.BadGateway
                        ||
                        ex.StatusCode == HttpStatusCode.ServiceUnavailable
                        ||
                        ex.StatusCode == HttpStatusCode.GatewayTimeout
                        ||
                        (ex.StatusCode == HttpStatusCode.Accepted && retryAfterHeader != null)
                        ||
                        (ex.StatusCode == HttpStatusCode.BadRequest
                        && errorCode != null && errorCode.Contains("invalid_grant")
                        && errorDescription != null && errorDescription.Contains("exp")))
                        && retryCounter++ < HttpRequestHandler.RetryLimit)
                    {

                        TimeSpan delay = expBackoff.GetRetryTimeout(retryCounter);

                        // If the response contains a Retry-After header, override the exponential back-off delay value
                        int timeToWait;
                        if (retryAfterHeader != null && int.TryParse(retryAfterHeader.ToString(), out timeToWait))
                        {
                            delay = new TimeSpan(0, 0, 0, 0, timeToWait);
                        }

                        // Before we retry the JWT Authentication request, we must regenerate the JTI claim with an updated DateTime.
                        // A delay is added to the JWT time, to account for the time of the upcoming wait.
                        var serverDate = ex.ResponseHeaders != null ? ex.ResponseHeaders.Date : null;
                        if (serverDate.HasValue)
                        {
                            var date = serverDate.Value;
                            assertion = ConstructJWTAssertion(subId, subType, date.LocalDateTime.Add(delay));
                        }
                        else
                        {
                            assertion = ConstructJWTAssertion(subId, subType, DateTime.UtcNow.Add(delay));
                        }

                        Debug.WriteLine("HttpCode: {0}. Waiting for {1} seconds to retry JWT Authentication request.", ex.StatusCode, delay.Seconds);
                        System.Threading.Tasks.Task.Delay(delay).Wait();
                    }
                    else
                    {
                        throw ex;
                    }
                } /**/
            }
        }

        /// <summary>
        /// Create OAuth session from token
        /// </summary>
        /// <param name="token">Access token created by method UserToken, or AdminToken</param>
        /// <returns>OAuth session</returns>
        public OAuthSession Session(string token)
        {
            return new OAuthSession(token, null, 3600, TOKEN_TYPE);
        }

        private string ConstructJWTAssertion(string sub, string boxSubType, DateTime? nowOverride = null)
        {
            byte[] randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            var claims = new List<Claim>{
                new Claim("sub", sub),
                new Claim("box_sub_type", boxSubType),
                new Claim("jti", Convert.ToBase64String(randomNumber)),
            };

            DateTime expireTime = DateTime.UtcNow.AddSeconds(30);
            if (nowOverride.HasValue)
            {
                expireTime = nowOverride.Value.AddSeconds(30);
            }

            var payload = new JwtPayload(this.boxConfig.ClientId, AUTH_URL, claims, null, expireTime);

            var header = new JwtHeader(signingCredentials: this.credentials);
            if (this.boxConfig.JWTPublicKeyId != null)
                header.Add("kid", this.boxConfig.JWTPublicKeyId);

            var token = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();
            string assertion = tokenHandler.WriteToken(token);
            return assertion;
        }

        private OAuthSession JWTAuthPost(string assertion)
        {
            BoxRequest boxRequest = new BoxRequest(this.boxConfig.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Header(Constants.RequestParameters.UserAgent, this.boxConfig.UserAgent)
                                            .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.JWTAuthorizationCode)
                                            .Payload(Constants.RequestParameters.Assertion, assertion)
                                            .Payload(Constants.RequestParameters.ClientId, this.boxConfig.ClientId)
                                            .Payload(Constants.RequestParameters.ClientSecret, this.boxConfig.ClientSecret);
            
            var converter = new BoxJsonConverter();
            IBoxResponse<OAuthSession> boxResponse = this.boxService.ToResponseAsyncWithoutRetry<OAuthSession>(boxRequest).Result;
            boxResponse.ParseResults(converter);

            return boxResponse.ResponseObject;
        }
    }

    class PEMPasswordFinder : IPasswordFinder
    {
        private string pword;
        public PEMPasswordFinder(string password) { pword = password; }
        public char[] GetPassword() { return pword.ToCharArray(); }
    }
}
