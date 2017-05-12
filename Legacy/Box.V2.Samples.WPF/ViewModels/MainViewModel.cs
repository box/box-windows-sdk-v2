using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Exceptions;
using Box.V2.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Box.V2.Samples.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public const string ClientId = "5uzq9vy86dsefo8rpmd60ol1i0hu54qr";
        public const string ClientSecret = "EPAjy3QrBczECOYDt2kXKBmjmJwcx6OE";

        public Uri RedirectUri = new Uri("https://boxsdk");

        public MainViewModel()
            : base()
        {
            OAuthSession session = null;

            Config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            Client = new BoxClient(Config, session);
        }

        /// <summary>
        /// Fetch access token and refresh token. Load the list of files and folders
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public async Task Init(string authCode)
        {
            await Client.Auth.AuthenticateAsync(authCode);
            
            var cmd = new AsyncDelegateCommand(async _ => { await GetFolderItemsAsync("0"); });
            ExecuteCommand(cmd);

            IsLoggedIn = true;
        }
                
        private ObservableCollection<BoxItem> _items = new ObservableCollection<BoxItem>();

        public ObservableCollection<BoxItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                PropertyChangedAsync("Items");
            }
        }

        public IBoxConfig Config { get; set; }

        public BoxClient Client { get; set; }

        private bool _isLoggedIn = false;

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    PropertyChangedAsync("IsLoggedIn");
                }
            }
        }

        public async Task GetFolderItemsAsync(string id)
        {
            Items.Clear();

            BoxCollection<BoxItem> results = await Client.FoldersManager.GetFolderItemsAsync((string)id, 100);

            foreach (BoxItem item in results.Entries)
            {
                Items.Add(item);
            }
        }

        private void ExecuteCommand(ICommand cmd){

            try
            {
                cmd.Execute(null);

            }
            catch (BoxException ex)
            {
                if (ex.Message.Equals("invalid_grant"))
                {
                    IsLoggedIn = false;
                    // logout user navigate to login page
                }
            }
        }
    }
}
