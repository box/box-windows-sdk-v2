using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Box.V2.Samples.WP.Resources;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using Box.V2.Samples.ViewModels;
using Box.V2.Samples.WP.Controls;

namespace Box.V2.Samples.WP
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
        IApplicationBar _appBar;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _main = ViewModelLocator.Main;

            this.ApplicationBar.IsVisible = false;
            oauth.AuthCodeReceived += async (s, e) =>
            {
                var auth = s as OAuth2Sample;
                if (auth == null)
                    return;

                await _main.Init(auth.AuthCode);

                Dispatcher.BeginInvoke(() =>
                {
                    this.ApplicationBar.IsVisible = true;
                });
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_main.IsLoggedIn)
                oauth.GetAuthCode(_main.Config.AuthCodeUri, _main.Config.RedirectUri);
        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_main.SelectedItem == null)
                return;

            switch (_main.SelectedItem.Type)
            {
                case "folder":
                    await _main.GetFolderItemsAsync(_main.SelectedItem.Id);
                    break;
            }
        }

        private void ItemPicker_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ItemPickerPage.xaml", UriKind.Relative));
        }
    }
}