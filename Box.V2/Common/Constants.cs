using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public static class Constants
    {
        public const string BoxApiHostUriString = "https://www.box.com/api/";
        public const string BoxUploadApiUriString = "https://upload.box.com/api/2.0";
        public const string BoxApiUriString = "https://api.box.com/2.0";

        public const string AuthCodeEndpointString = @"oauth2/authorize";
        public const string AuthTokenEndpointString = @"oauth2/token";
    }
}
