using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Auth
{
    public class OAuthSession
    {
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
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken { get; private set; }

        /// <summary>
        /// Refresh token used to exchange for a new access token. This token is only good 
        /// for one time use. Once used, both the current refresh token token and access token 
        /// will be invalidated.
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; private set; }
        
        /// <summary>
        /// Seconds the access token will be active
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; private set; }

        /// <summary>
        /// Represents how the access token will be generated and presented
        /// Most commonly this will be "bearer", anybody with a bearer token will have access
        /// </summary>
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; private set; }
    }
}
