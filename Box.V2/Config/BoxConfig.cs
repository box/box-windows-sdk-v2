using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Config
{
    public class BoxConfig : IBoxConfig
    {

        /// <summary>
        /// Instantiates a Box config with all of the standard defaults
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"></param>
        public BoxConfig(string clientId, string clientSecret, Uri redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        /// <summary>
        /// Instantiates a Box config for use with JWT authentication
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="enterpriseId"></param>
        /// <param name="jwtPrivateKey"></param>
        /// <param name="jwtPrivateKeyPassword"></param>
        /// <param name="jwtPublicKeyId"></param>
        public BoxConfig(string clientId, string clientSecret, string enterpriseId, 
            string jwtPrivateKey, string jwtPrivateKeyPassword, string jwtPublicKeyId)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            EnterpriseId = enterpriseId;
            JWTPrivateKey = jwtPrivateKey;
            JWTPrivateKeyPassword = jwtPrivateKeyPassword;
            JWTPublicKeyId = jwtPublicKeyId;
        }



        public virtual Uri BoxApiHostUri { get { return new Uri(Constants.BoxApiHostUriString); } }
        public virtual Uri BoxApiUri { get { return new Uri(Constants.BoxApiUriString); } }
        public virtual Uri BoxUploadApiUri { get { return new Uri(Constants.BoxUploadApiUriString); } }

        public virtual string ClientId { get; private set; }
        public virtual string ConsumerKey { get; private set; }
        public virtual string ClientSecret { get; private set; }
        public virtual Uri RedirectUri { get; set; }

        public string EnterpriseId { get; private set; }
        public string JWTPrivateKey { get; private set; }
        public string JWTPrivateKeyPassword { get; private set; }
        public string JWTPublicKeyId { get; private set; }

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string UserAgent { get; set; }

        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        public CompressionType? AcceptEncoding { get; set; }

        public virtual Uri AuthCodeBaseUri { get { return new Uri(BoxApiHostUri, Constants.AuthCodeString); } }
        public virtual Uri AuthCodeUri { get { return new Uri(AuthCodeBaseUri, string.Format("?response_type=code&client_id={0}&redirect_uri={1}", ClientId, RedirectUri)); } }
        public virtual Uri FoldersEndpointUri { get { return new Uri(BoxApiUri, Constants.FoldersString); } }
        public virtual Uri FilesEndpointUri { get { return new Uri(BoxApiUri, Constants.FilesString); } }
        public virtual Uri FilesUploadEndpointUri { get { return new Uri(BoxUploadApiUri, Constants.FilesUploadString); } }
        public virtual Uri FilesNewVersionEndpointUri { get { return new Uri(BoxUploadApiUri, Constants.FilesNewVersionString); } }
        public virtual Uri CommentsEndpointUri { get { return new Uri(BoxApiUri, Constants.CommentsString); } }
        public virtual Uri SearchEndpointUri { get { return new Uri(BoxApiUri, Constants.SearchString); } }
        public virtual Uri UserEndpointUri { get { return new Uri(BoxApiUri, Constants.UserString); } }
        public virtual Uri CollaborationsEndpointUri { get { return new Uri(BoxApiUri, Constants.CollaborationsString); } }
        public virtual Uri GroupsEndpointUri { get { return new Uri(BoxApiUri, Constants.GroupsString); } }
        public virtual Uri GroupMembershipEndpointUri { get { return new Uri(BoxApiUri, Constants.GroupMembershipString); } }
        public virtual Uri RetentionPoliciesEndpointUri {  get { return new Uri(BoxApiUri, Constants.RetentionPoliciesString); } }
        public virtual Uri RetentionPolicyAssignmentsUri {  get { return new Uri(BoxApiUri, Constants.RetentionPolicyAssignmentsString); } }
        public virtual Uri FileVersionRetentionsUri { get { return new Uri(BoxApiUri, Constants.FileVersionRetentionsString); } }
    }

    public enum CompressionType
    {
        gzip, 
        deflate
    }
}
