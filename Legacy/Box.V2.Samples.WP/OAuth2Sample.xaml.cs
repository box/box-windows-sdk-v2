using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Box.V2.Samples.WP
{
    public partial class OAuth2Sample : UserControl
    {
        public OAuth2Sample()
        {
            InitializeComponent();
        }

        private Uri _redirectUri;

        public event EventHandler AuthCodeReceived;

        public string AuthCode { get; private set; }

        public void GetAuthCode(Uri authUri, Uri redirectUri)
        {
            _redirectUri = redirectUri;
            oauthBrowser.Navigate(authUri);
            oauthBrowser.Visibility = Visibility.Visible;
        }

        private void oauthBrowser_Navigating(object sender, NavigatingEventArgs e)
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
    }
}