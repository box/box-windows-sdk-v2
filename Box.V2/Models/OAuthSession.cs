using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Auth
{
    public class OAuthSession
    {
        private const string FieldAccessToken = "access_token";
        private const string FieldRefreshToken = "refresh_token";
        private const string FieldExpiresIn = "expires_in";
        private const string FieldTokenType = "token_type";


        public OAuthSession(string access_token, string refresh_token, int expires_in, string token_type)
        {
            AccessToken = access_token;
            RefreshToken = refresh_token;
            ExpiresIn = expires_in;
            TokenType = token_type;
        }

        /// <summary>
        /// The token used to retrieve all data that requires authorization
        /// </summary>
        [JsonProperty(PropertyName = FieldAccessToken)]
        public string AccessToken { get; private set; }

        /// <summary>
        /// Refresh token used to exchange for a new access token. This token is only good 
        /// for one time use. Once used, both the current refresh token token and access token 
        /// will be invalidated.
        /// </summary>
        [JsonProperty(PropertyName = FieldRefreshToken)]
        public string RefreshToken { get; private set; }

        /// <summary>
        /// Seconds the access token will be active
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresIn)]
        public int ExpiresIn { get; private set; }

        /// <summary>
        /// Represents how the access token will be generated and presented
        /// Most commonly this will be "bearer", anybody with a bearer token will have access
        /// </summary>
        [JsonProperty(PropertyName = FieldTokenType)]
        public string TokenType { get; private set; }
    }
}
