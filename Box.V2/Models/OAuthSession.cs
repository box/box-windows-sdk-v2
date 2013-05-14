using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Auth
{
    public class OAuthSession
    {
        /// <summary>
        /// The token used to retrieve all data that requires authorization
        /// </summary>
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Refresh token used to exchange for a new access token. This token is only good 
        /// for one time use. Once used, both the current refresh token token and access token 
        /// will be invalidated.
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        
        /// <summary>
        /// Seconds the access token will be active
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }
}
