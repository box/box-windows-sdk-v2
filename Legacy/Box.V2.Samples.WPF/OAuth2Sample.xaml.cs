using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Box.V2.Samples.WPF
{
    /// <summary>
    /// Interaction logic for OAuth2Sample.xaml
    /// </summary>
    public partial class OAuth2Sample : UserControl
    {
        private Uri _redirectUri;
        public event EventHandler AuthCodeReceived;

        public OAuth2Sample()
        {
            InitializeComponent();
        }

        public string AuthCode { get; private set; }

        public void GetAuthCode(Uri authUri, Uri redirectUri)
        {
            _redirectUri = redirectUri;
            oauthBrowser.Navigate(authUri);
            oauthBrowser.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// If redirected to Redirect URL (as set in the application), this method extracts the Authentication code
        /// which will be used in the viewmodel to get the access token and refresh token.
        /// </summary>
        private void oauthBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri.Host.Equals(_redirectUri.Host))
            {
                e.Cancel = true;

                // grab access_token and oauth_verifier
                IDictionary<string, string> keyDictionary = new Dictionary<string, string>();
                var qSplit = e.Uri.Query.Split('?');
                foreach (var kvp in qSplit[qSplit.Length - 1].Split('&'))
                {
                    var kvpSplit = kvp.Split('=');
                    if (kvpSplit.Length == 2)
                    {
                        keyDictionary.Add(kvpSplit[0], kvpSplit[1]);
                    }
                }

                AuthCode = keyDictionary["code"];

                if (AuthCodeReceived != null)
                {
                    AuthCodeReceived(this, new EventArgs() { });
                    oauthBrowser.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void oauthBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            string script = "document.body.style.overflow ='hidden'";
            WebBrowser wb = (WebBrowser)sender;
            wb.InvokeScript("execScript", new Object[] { script, "JavaScript" });
        }
    }
}
