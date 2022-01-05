using System;
using Box.V2.Utility;

namespace Box.V2.Auth.Token
{
    /// <summary>
    /// Builder for building an actor token.
    /// </summary>
    public class ActorTokenBuilder
    {
        private const string ACTOR_HEADER = "{\"alg\":\"none\"}";
        private const string ACTOR_CLAIM_TEMPLATE = "{{\"iss\":\"{0}\",\"sub\":\"{1}\",\"box_sub_type\":\"external\",\"aud\":\"https://api.box.com/oauth2/token\",\"exp\":{2},\"jti\":\"{3}\",\"name\":\"{4}\"}}";

        /// <summary>
        /// External user id.
        /// </summary>
        protected string userId;
        /// <summary>
        /// External user name.
        /// </summary>
        protected string userName;

        /// <summary>
        /// Client id.
        /// </summary>
        protected string clientId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        public ActorTokenBuilder(string userId, string clientId)
        {
            this.userId = userId;
            this.clientId = clientId;
        }

        /// <summary>
        /// Set external user name.
        /// </summary>
        /// <param name="userName">The external user name.</param>
        /// <returns>Current builder instance.</returns>
        public ActorTokenBuilder SetUserName(string userName)
        {
            this.userName = userName;
            return this;
        }

        /// <summary>
        /// Build the actorToken.
        /// </summary>
        /// <returns>The actorToken in string.</returns>
        public string Build()
        {
            var exp = Helper.ConvertToUnixTimestamp(DateTimeOffset.UtcNow.AddSeconds(30));
            var jti = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var header = Helper.Base64Encode(ACTOR_HEADER);

            var claims = string.Format(ACTOR_CLAIM_TEMPLATE, clientId, userId, exp, jti, userName);
            claims = Helper.Base64Encode(claims);

            return header + "." + claims + ".";
        }
    }
}
