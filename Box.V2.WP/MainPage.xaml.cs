using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Box.V2.WP.Resources;
using Box.V2.Contracts;

namespace Box.V2.WP
{
    public partial class MainPage : PhoneApplicationPage
    {

        public const string ClientId = "pweqblqwil7cpmvgu45jaokt3qw77wbo";
        public const string ClientSecret = "dTrKxu2JYDeYIyQKSKLDf57HVlWjvU10";

        // Ryan's Dev keys
        //private const string ClientId = "yrizdmqzb9jw4bf6c3cged90xyjyzlzy";
        //public const string ClientSecret = "c6vRohbuxHCn7ol6yDdho6prcQg0buRJ";

        public const string RedirectUri = @"http://localhost";

        IBoxConfig _config;
        BoxClient _client;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config);
        }



        private void oauthBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.Host.Equals("boxsdk")) // in our case we used localhost as the redirect_uri
            {
                oauthBrowser.Visibility = Visibility.Collapsed;
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

                _client.Auth.AuthenticateAsync(keyDictionary["code"]);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            oauthBrowser.Navigate(_client.Auth.AuthCodeUri);
        }
    }
}