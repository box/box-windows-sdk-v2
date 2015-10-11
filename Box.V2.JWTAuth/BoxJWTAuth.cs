using Jose;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Box.V2.JWTAuth
{
    public class BoxJWTAuth
    {
        const string AUTH_URL = "https://api.box.com/oauth2/token";
        const string JWT_GRANT_TYPE = "urn:ietf:params:oauth:grant-type:jwt-bearer";
        const string ENTERPRISE_SUB_TYPE = "enterprise";
        const string USER_SUB_TYPE = "user";

        private readonly string enterpriseId;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string publicKeyId;
        private readonly RSA credentials;

        public BoxJWTAuth(string enterpriseId, string clientId, string clientSecret, string privateKey, string privateKeyPassword, string publicKeyId)
        {
            this.enterpriseId = enterpriseId;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.publicKeyId = publicKeyId;

            var pwf = new PEMPasswordFinder(privateKeyPassword);
            AsymmetricCipherKeyPair key;
            using (var reader = new StringReader(privateKey))
            {
                key = (AsymmetricCipherKeyPair)new PemReader(reader, pwf).ReadObject();
            }
            this.credentials = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)key.Private);
        }

        public string EnterpriseToken()
        {
            var assertion = ConstructJWTAssertion(this.enterpriseId, ENTERPRISE_SUB_TYPE);

            var result = JWTAuthPost(assertion);
            return result.access_token;
        }

        public string UserToken(string userId)
        {
            var assertion = ConstructJWTAssertion(userId, USER_SUB_TYPE);

            var result = JWTAuthPost(assertion);
            return result.access_token;
        }

        private string ConstructJWTAssertion(string sub, string boxSubType)
        {
            byte[] randomNumber = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
            }

            Int32 expiresInUnixTimestamp = (Int32)(DateTime.UtcNow.AddSeconds(10).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var payload = new Dictionary<string, object>()
            {
                { "iss", clientId },
                { "sub", sub },
                { "box_sub_type", boxSubType },
                { "aud", AUTH_URL },
                { "jti", Convert.ToBase64String(randomNumber) },
                { "exp", expiresInUnixTimestamp }
            };

            var headers = new Dictionary<string, object>();
            if (publicKeyId != null)
            {
                headers.Add("kid", publicKeyId);
            }

            string assertion = JWT.Encode(payload, this.credentials, JwsAlgorithm.RS256, extraHeaders: headers);
            return assertion;
        }

        private dynamic JWTAuthPost(string assertion)
        {
            var client = new RestClient(AUTH_URL);
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", JWT_GRANT_TYPE);
            request.AddParameter("client_id", this.clientId);
            request.AddParameter("client_secret", this.clientSecret);
            request.AddParameter("assertion", assertion);

            var response = client.Execute(request);
            var content = response.Content;

            dynamic parsed_content = JObject.Parse(content);
            return parsed_content;
        }
    }

    class PEMPasswordFinder : IPasswordFinder
    {
        private string pword;
        public PEMPasswordFinder(string password){pword = password;}
        public char[] GetPassword(){return pword.ToCharArray();}
    }
}
