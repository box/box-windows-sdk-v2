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

namespace Box.V2.JWTAuth
{
    ///<summary>
    /// Box’s new authentication model allows applications to authenticate directly to Box using a JSON Web Token (JWT) signed with an RSA key. This authentication method is meant for server-to-server applications and replaces the first leg of the standard 3-legged OAuth 2.0 process in which users grant an application authorization to access their Box account.
    ///</summary>
    ///<remarks>
    ///https://docs.box.com/docs/getting-started-box-platform
    ///</remarks>
    public class BoxJWTAuth
    {
        const string AUTH_URL = "https://api.box.com/oauth2/token";
        const string ENTERPRISE_SUB_TYPE = "enterprise";
        const string USER_SUB_TYPE = "user";
        const string TOKEN_TYPE = "bearer";

        readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1);

        private readonly IBoxConfig boxConfig;
        private readonly SigningCredentials credentials;
        /// <summary>
        /// Constructor for JWT authentication
        /// </summary>
        /// <param name="boxConfig">Config contains information about client id, client secret, enterprise id, private key, private key password, public key id </param>
        public BoxJWTAuth(IBoxConfig boxConfig)
        {
            this.boxConfig = boxConfig;

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
            var assertion = ConstructJWTAssertion(this.boxConfig.EnterpriseId, ENTERPRISE_SUB_TYPE);
            var result = JWTAuthPost(assertion);
            return result.AccessToken;
        }
        /// <summary>
        /// Once you have created an App User, you can request a User Access Token via the App Auth feature, which will return the OAuth 2.0 access token for the specified App User.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>User token</returns>
        public string UserToken(string userId)
        {
            var assertion = ConstructJWTAssertion(userId, USER_SUB_TYPE);
            var result = JWTAuthPost(assertion);
            return result.AccessToken;
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

        private string ConstructJWTAssertion(string sub, string boxSubType)
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

            var payload = new JwtPayload(this.boxConfig.ClientId, AUTH_URL, claims, null, DateTime.UtcNow.AddSeconds(30));

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

            var handler = new HttpRequestHandler();
            var converter = new BoxJsonConverter();
            var service = new BoxService(handler);

            IBoxResponse<OAuthSession> boxResponse = service.ToResponseAsync<OAuthSession>(boxRequest).Result;
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
