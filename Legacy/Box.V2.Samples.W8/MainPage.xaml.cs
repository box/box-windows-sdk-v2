using Box.V2.Auth;
using Box.V2.Models;
using Box.V2.Services;
using Box.V2.Samples.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Box.V2.Samples.W8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainViewModel _main;

        public MainPage()
        {
            this.InitializeComponent();

            _main = ViewModelLocator.Main;

            // Attach the event handler for when an item is selected 
            boxFilePicker.ItemSelected += boxItemPicker_ItemSelected;
            boxFolderPicker.ItemSelected += boxItemPicker_ItemSelected;
        }

        async void boxItemPicker_ItemSelected(object sender, BoxItem e)
        {
            if (e != null)
            {
                MessageDialog dialog = new MessageDialog(string.Format("{0} Selected!", e.Name));
                await dialog.ShowAsync();
            }
            
        }

        private async void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_main.ParentId))
                await _main.GetFolderItemsAsync(_main.ParentId);
        }

        private async void FolderView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as BoxItem;
            if (item == null || item.Type != "folder")
                return;

            await _main.GetFolderItemsAsync(item.Id);
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await _main.GetFolderItemsAsync(_main.FolderId);
        }

        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            await _main.Download();
        }

        private async void Upload_Click(object sender, RoutedEventArgs e)
        {
            await _main.Upload();
        }

        private async void mainPage_Loaded(object sender, RoutedEventArgs e)
        {
            string authCode = await OAuth2Sample.GetAuthCode(_main.Config.AuthCodeUri, _main.Config.RedirectUri);
            await _main.Init(authCode);
        }

    }
}
