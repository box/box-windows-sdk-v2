using Box.V2.Contracts;
using Box.V2.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using System.ComponentModel;

namespace Box.V2.Auth
{
    public class OAuthWorkflow 
    {
        private IBoxConfig _boxConfig;
        private string _redirectUri;

        public OAuthWorkflow(IBoxConfig boxConfig, string redirectUri)
        {
            _boxConfig = boxConfig;
            _redirectUri = redirectUri;
        }

        public Uri AuthCodeUri
        {
            get
            {
                return new BoxRequest(RequestMethod.GET, _boxConfig.BoxApiHostUri, Constants.AuthCodeEndpointString)
                        .Param("response_type", "code")
                        .Param("client_id", _boxConfig.ClientId)
                        .Param("redirect_uri", _redirectUri)
                        .AbsoluteUri;
            }
        }


    }
}
