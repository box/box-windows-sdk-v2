using Jose;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
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

        public static string EnterpriseToken(string enterpriseId, string clientId, string clientSecret, string privateKey, string privateKeyPassword, string publicKeyId=null)
        {
            byte[] randomNumber = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
            }

            Int32 expiresInUnixTimestamp = (Int32)(DateTime.UtcNow.AddSeconds(30).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var payload = new Dictionary<string, object>()
            {
                { "iss", clientId },
                { "sub", enterpriseId },
                { "box_sub_type", "enterprise" },
                { "aud", AUTH_URL },
                { "jti", Convert.ToBase64String(randomNumber) },
                { "exp", expiresInUnixTimestamp }
            };

            var headers = new Dictionary<string, object>();
            if (publicKeyId != null) {
                headers.Add("kid", publicKeyId);
            }

            var pwf = new PEMPasswordFinder(privateKeyPassword);
            AsymmetricCipherKeyPair key;
            using (var reader = new StringReader(privateKey))
            {
                key = (AsymmetricCipherKeyPair)new PemReader(reader, pwf).ReadObject();
            }
            var rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)key.Private);

            string assertion = JWT.Encode(payload, rsa, JwsAlgorithm.RS256, extraHeaders: headers);

            return assertion;
        }
    }

    class PEMPasswordFinder : IPasswordFinder
    {
        private string pword;
        public PEMPasswordFinder(string password){pword = password;}
        public char[] GetPassword(){return pword.ToCharArray();}
    }
}
