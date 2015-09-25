using Jose;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.PlatformEdition
{
    public class BoxPlatformEdition
    {
        const string JWT_GRANT_TYPE = "urn:ietf:params:oauth:grant-type:jwt-bearer";

        public static string EnterpriseToken(string enterpriseId, string clientId, string clientSecret, string privateKey, string privateKeyPassword)
        {
            var payload = new Dictionary<string, object>()
            {
                { "sub", "mr.x@contoso.com" },
                { "exp", 1300819380 }
            };

            var pwf = new PEMPasswordFinder(privateKeyPassword);
            AsymmetricCipherKeyPair key;
            using (var reader = new StringReader(privateKey))
            {
                key = (AsymmetricCipherKeyPair)new PemReader(reader, pwf).ReadObject();
            }
            var rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)key.Private);

            string token = JWT.Encode(payload, rsa, JwsAlgorithm.RS256);

            return "token";
        }
    }

    class PEMPasswordFinder : IPasswordFinder
    {
        private string pword;

        public PEMPasswordFinder(string password)
        {
            pword = password;
        }

        public char[] GetPassword()
        {
            return pword.ToCharArray();
        }
    }
}
