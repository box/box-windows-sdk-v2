using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Request;
using Box.V2.Services;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Box.V2.JWTAuth
{
    public class BoxJWTAuth
    {
        const string AUTH_URL = "https://api.box.com/oauth2/token";
        const string ENTERPRISE_SUB_TYPE = "enterprise";
        const string USER_SUB_TYPE = "user";
        const string TOKEN_TYPE = "bearer";

        readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1);

        private readonly IBoxConfig boxConfig;
        private readonly SigningCredentials credentials;

        public BoxJWTAuth(IBoxConfig boxConfig)
        {
            this.boxConfig = boxConfig;

            var pwf = new PEMPasswordFinder(this.boxConfig.JWTPrivateKeyPassword);
            AsymmetricCipherKeyPair key;
            using (var reader = new StringReader(this.boxConfig.JWTPrivateKey))
            {
                key = (AsymmetricCipherKeyPair)new PemReader(reader, pwf).ReadObject();
            }
            var rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)key.Private);

            this.credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);
        }

        public BoxClient AdminClient(string adminToken)
        {
            var adminSession = this.Session(adminToken);
            var authRepo = new JWTAuthRepository(adminSession, this);
            var adminClient = new BoxClient(this.boxConfig, authRepo);

            return adminClient;
        }

        public BoxClient UserClient(string userToken, string userId)
        {
            var userSession = this.Session(userToken);
            var authRepo = new JWTAuthRepository(userSession, this, userId);
            var userClient = new BoxClient(this.boxConfig, authRepo);

            return userClient;
        }

        public string AdminToken()
        {
            var assertion = ConstructJWTAssertion(this.boxConfig.EnterpriseId, ENTERPRISE_SUB_TYPE);
            var result = JWTAuthPost(assertion);
            return result.AccessToken;
        }

        public string UserToken(string userId)
        {
            var assertion = ConstructJWTAssertion(userId, USER_SUB_TYPE);
            var result = JWTAuthPost(assertion);
            return result.AccessToken;
        }

        public OAuthSession Session(string token)
        {
            return new OAuthSession(token, null, 3600, TOKEN_TYPE);
        }

        private string ConstructJWTAssertion(string sub, string boxSubType)
        {
            byte[] randomNumber = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
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
        public PEMPasswordFinder(string password){pword = password;}
        public char[] GetPassword(){return pword.ToCharArray();}
    }
}
