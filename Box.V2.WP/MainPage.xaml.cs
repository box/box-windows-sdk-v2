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
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using Box.V2.Sample.ViewModels;

namespace Box.V2.WP
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Keys on Live
        public const string ClientId = "pweqblqwil7cpmvgu45jaokt3qw77wbo";
        public const string ClientSecret = "dTrKxu2JYDeYIyQKSKLDf57HVlWjvU10";

        // Keys on Dev
        //public const string ClientId = "2simanymqjyz8hgnd5xzv0ayjdl5dhps";
        //public const string ClientSecret = "3BOQj9pOC2z01YhG17pCHw74fmmH9qqs";

        public const string RedirectUri = @"http://localhost";


        MainViewModel _main;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _main = ViewModelLocator.Main;

            oauth.AuthCodeReceived += async (s, e) =>
            {
                var auth = s as OAuth2Sample;
                if (auth == null)
                    return;

                await _main.Init(auth.AuthCode);
                //NavigationService.Navigate(new Uri("/PreviewPage.xaml", UriKind.Relative));
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            oauth.GetAuthCode(_main.Config.AuthCodeUri, _main.Config.RedirectUri);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_main.SelectedItem == null)
                return;

            switch (_main.SelectedItem.Type)
            {
                case "folder":
                    _main.GetFolderItems(_main.SelectedItem.Id);
                    break;
                case "file":
                    NavigationService.Navigate(new Uri("/PreviewPage.xaml", UriKind.Relative));
                    break;
            }
        }
    }
}