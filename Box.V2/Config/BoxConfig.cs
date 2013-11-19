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
        /// <param name="redirectUriString"></param>
        public BoxConfig(string clientId, string clientSecret, Uri redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        public Uri BoxApiHostUri { get { return new Uri(Constants.BoxApiHostUriString); } }
        public Uri BoxApiUri { get { return new Uri(Constants.BoxApiUriString); } }
        public Uri BoxUploadApiUri { get { return new Uri(Constants.BoxUploadApiUriString); } }

        public string ClientId { get; private set; }
        public string ConsumerKey { get; private set; }
        public string ClientSecret { get; private set; }
        public Uri RedirectUri { get; set; }

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string UserAgent { get; set; }

        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        public CompressionType? AcceptEncoding { get; set; }

        public Uri AuthCodeUri { get { return new Uri(string.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}", Constants.AuthCodeEndpointString, ClientId, RedirectUri)); } }
            
        public Uri FoldersEndpointUri { get { return new Uri(Constants.FoldersEndpointString); } }
        public Uri FilesEndpointUri { get { return new Uri(Constants.FilesEndpointString); } }
        public Uri FilesUploadEndpointUri { get { return new Uri(Constants.FilesUploadEndpointString); } }
        public Uri CommentsEndpointUri { get { return new Uri(Constants.CommentsEndpointString); } }
        public Uri SearchEndpointUri { get { return new Uri(Constants.SearchEndpointString); } }
        public Uri UserEndpointUri { get { return new Uri(Constants.UserEndpointString); } }
        public Uri CollaborationsEndpointUri { get { return new Uri(Constants.CollaborationsEndpointString); } }
        public Uri GroupsEndpointUri { get { return new Uri(Constants.GroupsEndpointString); } }
        public Uri GroupMembershipEndpointUri { get { return new Uri(Constants.GroupMembershipEndpointString); } }
    }

    public enum CompressionType
    {
        gzip, 
        deflate
    }
}
