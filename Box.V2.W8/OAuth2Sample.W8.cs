using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Authentication.Web;

namespace Box.V2.W8
{
    public static class OAuth2Sample
    {
        public static async Task<string> GetAuthCode(Uri authCodeUri, Uri redirectUri)
        {

            WebAuthenticationResult war = await WebAuthenticationBroker.AuthenticateAsync(
                WebAuthenticationOptions.None,
                authCodeUri,
                redirectUri);

            switch (war.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    {
                        // grab auth code
                        var response = war.ResponseData;
                        WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(new Uri(response).Query);
                        return decoder.GetFirstValueByName("code");
                    }
                case WebAuthenticationStatus.UserCancel:
                    {

                        //log("HTTP Error returned by AuthenticateAsync() : " + war.ResponseErrorDetail.ToString());
                        break;
                    }
                default:
                case WebAuthenticationStatus.ErrorHttp:
                    //log("Error returned by AuthenticateAsync() : " + war.ResponseStatus.ToString());
                    break;
            }

            return string.Empty;
        }
    }
}
