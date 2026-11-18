using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class JwtAccessTokenDataFactory : IRequestDataFactory {
        /// <summary>
        /// JWT configuration used to sign the assertion
        /// </summary>
        internal JwtConfig Config { get; }

        /// <summary>
        /// The ID of the user or enterprise to authenticate as
        /// </summary>
        internal string? SubjectId { get; init; }

        /// <summary>
        /// The type of the subject ID, either 'user' or 'enterprise'
        /// </summary>
        internal string? SubjectType { get; init; }

        public JwtAccessTokenDataFactory(JwtConfig config) {
            Config = config;
        }
        /// <summary>
        /// Creates the access token request body with a newly signed JWT assertion.
        /// </summary>
        public SerializedData Create() {
            JwtAlgorithm alg = this.Config.Algorithm != null ? NullableUtils.Unwrap(this.Config.Algorithm) : JwtAlgorithm.Rs256;
            Dictionary<string, object> claims = new Dictionary<string, object>() { { "exp", Utils.GetEpochTimeInSeconds() + 30 }, { "box_sub_type", this.SubjectType } };
            JwtSignOptions jwtOptions = new JwtSignOptions(algorithm: alg, audience: "https://api.box.com/oauth2/token", subject: this.SubjectId, issuer: this.Config.ClientId, jwtid: Utils.GetUUID(), keyid: this.Config.JwtKeyId, privateKeyDecryptor: this.Config.PrivateKeyDecryptor);
            JwtKey jwtKey = new JwtKey(key: this.Config.PrivateKey, passphrase: this.Config.PrivateKeyPassphrase);
            string assertion = JwtUtils.CreateJwtAssertion(claims: claims, key: jwtKey, options: jwtOptions);
            PostOAuth2Token requestBody = new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.UrnIetfParamsOauthGrantTypeJwtBearer) { Assertion = assertion, ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret };
            return SimpleJsonSerializer.Serialize(requestBody);
        }

    }
}