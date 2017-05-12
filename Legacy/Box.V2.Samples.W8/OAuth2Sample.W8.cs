using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Authentication.Web;

namespace Box.V2.Samples.W8
{
    /// <summary>
    /// A sample Windows 8 OAuth2 implementation. 
    /// </summary>
    public static class OAuth2Sample
    {
        /// <summary>
        /// Performs the first step in the OAuth2 workflow and retreives the auth code
        /// </summary>
        /// <param name="authCodeUri">The box api uri to retrieve the auth code. BoxConfig.AuthCodeUri should be used for this field</param>
        /// <param name="redirectUri">The redirect uri that the page will navigate to after granting the auth code</param>
        /// <returns></returns>
        public static async Task<string> GetAuthCode(Uri authCodeUri, Uri redirectUri = null)
        {
            Uri callbackUri = redirectUri == null ? 
                WebAuthenticationBroker.GetCurrentApplicationCallbackUri() :
                redirectUri;

            WebAuthenticationResult war = await WebAuthenticationBroker.AuthenticateAsync(
                WebAuthenticationOptions.None,
                authCodeUri,
                callbackUri);

            switch (war.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                        // grab auth code
                        var response = war.ResponseData;
                        WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(new Uri(response).Query);
                        return decoder.GetFirstValueByName("code");
                case WebAuthenticationStatus.UserCancel:
                        throw new Exception("User Canceled Login");
                case WebAuthenticationStatus.ErrorHttp:
                default:
                    throw new Exception("Error returned by GetAuthCode() : " + war.ResponseStatus.ToString());
            }
        }
    }
}
