using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Box.V2.WP
{
    public partial class OAuth2SamplePage : PhoneApplicationPage
    {
        public OAuth2SamplePage()
        {
            InitializeComponent();
        }

        private void oauthBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {

        }

        private async void oauthBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.Host.Equals("localhost")) // in our case we used localhost as the redirect_uri
            {
                //oauthBrowser.Visibility = Visibility.Collapsed;
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

                await _client.Auth.AuthenticateAsync(keyDictionary["code"]);
                
            }
    }
}