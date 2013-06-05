using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Contracts
{
    public class BoxConfig : IBoxConfig
    {

        public BoxConfig(string clientId, string clientSecret, string redirectUri)
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
        public string RedirectUri { get; set; }

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string UserAgent { get; set; }

        public Uri AuthCodeUri { get { return new Uri(string.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}", Constants.AuthCodeEndpointString, ClientId, RedirectUri)); } }
            
        public Uri FoldersEndpointUri { get { return new Uri(Constants.FoldersEndpointString); } }
        public Uri FilesEndpointUri { get { return new Uri(Constants.FilesEndpointString); } }
        public Uri FilesUploadEndpointUri { get { return new Uri(Constants.FilesUploadEndpointString); } }
    }
}
