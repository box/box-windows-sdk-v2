using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Box.Sdk.Gen.Internal
{
    /// <summary>
    /// Class for various jwt utilities functions used in SDK.
    /// </summary>
    static class JwtUtils
    {
        /// <summary>
        /// Create jwt assertion.
        /// </summary>JwtSignOptions
        /// <param name="claims">Jwt claims</param>
        /// <param name="key">Jwt key</param>
        /// <param name="options">Jwt sign options</param>
        /// <returns>Jwt assertion</returns>
        internal static string CreateJwtAssertion(Dictionary<string, object> claims, JwtKey key, JwtSignOptions options)
        {
            var jwtClaims = claims.Select(x => new Claim(x.Key, x.Value.ToString()!)).ToList();

            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            jwtClaims.Add(new Claim("jti", Convert.ToBase64String(randomNumber)));
            jwtClaims.Add(new Claim("sub", options.Subject));

            var expClaim = claims["exp"].ToString();
            if (string.IsNullOrEmpty(expClaim))
            {
                throw new BoxSdkException("Exp claim cannot be empty");
            }

            var expTime = long.Parse(expClaim);

            var jwtPayload = new JwtPayload(options.Issuer, options.Audience,
                jwtClaims, null, DateTimeOffset.FromUnixTimeSeconds(expTime).LocalDateTime);

            var signingCredentials = GetSigningCredentials(key, options);

            var header = new JwtHeader(signingCredentials);

            var token = new JwtSecurityToken(header, jwtPayload);
            var assertion = new JwtSecurityTokenHandler().WriteToken(token);

            return assertion;
        }

        private static SigningCredentials GetSigningCredentials(JwtKey key, JwtSignOptions options)
        {
            var rsa = options.PrivateKeyDecryptor.DecryptPrivateKey(key.Key, key.Passphrase);

            var rsaKey = new RsaSecurityKey(rsa);

            return new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);
        }

    }

    enum JwtAlgorithm
    {
        Rs256
    }

    class JwtKey
    {
        internal string Key { get; }
        internal string Passphrase { get; }

        public JwtKey(string key, string passphrase)
        {
            Key = key;
            Passphrase = passphrase;
        }
    }

    class JwtSignOptions
    {
        internal JwtAlgorithm Algorithm { get; }
        internal string Audience { get; }
        internal string Subject { get; }
        internal string Issuer { get; }
        internal string Jwtid { get; }
        internal string Keyid { get; }
        internal IPrivateKeyDecryptor PrivateKeyDecryptor { get; }

        public JwtSignOptions(JwtAlgorithm algorithm, string audience, string subject, string issuer, string jwtid, string keyid, IPrivateKeyDecryptor privateKeyDecryptor)
        {
            Algorithm = algorithm;
            Audience = audience;
            Subject = subject;
            Issuer = issuer;
            Jwtid = jwtid;
            Keyid = keyid;
            PrivateKeyDecryptor = privateKeyDecryptor;
        }
    }
}
