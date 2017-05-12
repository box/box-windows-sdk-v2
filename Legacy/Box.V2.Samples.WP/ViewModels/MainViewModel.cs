using Box.V2.Auth;
using Box.V2.Models;
using Box.V2.Services;
using Box.V2.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Box.V2.Config;

#if WINDOWS_PHONE
using System.Windows.Media.Imaging;
using System.Threading;
#else
using Windows.Networking.BackgroundTransfer;
#endif

namespace Box.V2.Samples.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";

        public Uri RedirectUri = new Uri("https://boxsdk");
        public readonly int ItemLimit = 100;

        public MainViewModel() : base() {
            OAuthSession session = null;

            Config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            Client = new BoxClient(Config, session);
        }

        #region Properties

        public IBoxConfig Config { get; set; }
        public BoxClient Client { get; set; }

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

        private string _folderName;
        public string FolderName
        {
            get
            {
                return _folderName;
            }
            set
            {
                if (_folderName != value)
                {
                    _folderName = value;
                    PropertyChangedAsync("FolderName");
                }
            }
        }

        private string _folderId;
        public string FolderId
        {
            get { return _folderId; }
            set
            {
                if (_folderId != value)
                {
                    _folderId = value;
                    PropertyChangedAsync("FolderId");
                }
            }
        }


        private string _parentId;
        public string ParentId
        {
            get { return _parentId; }
            set
            {
                if (_parentId != value)
                {
                    _parentId = value;
                    PropertyChangedAsync("ParentId");
                }
            }
        }

        private BoxItem _selectedItem;
        public BoxItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    IsItemSelected = _selectedItem != null;
                    PropertyChangedAsync("SelectedItem");
                }
            }
        }

        private bool _isItemSelected;
        public bool IsItemSelected
        {
            get { return _isItemSelected; }
            set
            {
                if (_isItemSelected != value)
                {
                    _isItemSelected = value;
                    PropertyChangedAsync("IsItemSelected");
                }
            }
        }


        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    PropertyChangedAsync("IsLoading");
                }
            }
        }

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
        

        #endregion

        public async Task Init(string authCode)
        {
            var session = await Client.Auth.AuthenticateAsync(authCode);

            IsLoggedIn = true;
            // Get the root folder
            await GetFolderItemsAsync("0");
        }

        public async Task GetFolderItemsAsync(string id)
        {
            Items.Clear();
            FolderName = string.Empty;
            int itemCount = 0;
            IsLoading = true;

            BoxFolder folder;
            do
            {
                folder = await Client.FoldersManager.GetItemsAsync(id, ItemLimit, itemCount);
                IsLoading = false;
                if (folder == null)
                {
                    string message = "Unable to get folder items. Please try again later";
                    break;
                }

                // Is first time in loop
                if (itemCount == 0)
                {
                    FolderName = folder.Name;
                    FolderId = folder.Id;
                    if (folder.PathCollection != null && folder.PathCollection.TotalCount > 0)
                    {
                        var parent = folder.PathCollection.Entries.LastOrDefault();
                        ParentId = parent == null ? string.Empty : parent.Id;
                    }
                }

                foreach (var i in folder.ItemCollection.Entries)
                {
                    Items.Add(i);
                }
                itemCount += ItemLimit;
            } while (itemCount < folder.ItemCollection.TotalCount);
        }

#if NETFX_CORE

        internal async Task BackgroundDownload()
        {
            if (SelectedItem == null)
                await new MessageDialog("No File Selected").ShowAsync();

            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedFileName = SelectedItem.Name;
            var ext = Path.GetExtension(SelectedItem.Name);
            fileSavePicker.FileTypeChoices.Add(ext, new string[] { ext });
            StorageFile saveFile = await fileSavePicker.PickSaveFileAsync();
            if (saveFile == null)
                return;

            var bgDownloader = new BackgroundDownloader();
            var test = Path.Combine(Config.FilesEndpointUri.ToString(), string.Format(Constants.ContentPathString, SelectedItem.Id));
            bgDownloader.SetRequestHeader(Constants.AuthHeaderKey, string.Format(Constants.V2AuthString, Client.Auth.Session.AccessToken));
            var download = bgDownloader.CreateDownload(new Uri(test), saveFile);

            var testRes = await download.StartAsync();
        }


        internal async Task Download()
        {
            if (SelectedItem == null)
                await new MessageDialog("No File Selected").ShowAsync();

            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedFileName = SelectedItem.Name;
            var ext = Path.GetExtension(SelectedItem.Name);
            fileSavePicker.FileTypeChoices.Add(ext, new string[] { ext });
            StorageFile saveFile = await fileSavePicker.PickSaveFileAsync();
            if (saveFile == null)
                return;

            using (Stream dataStream = await Client.FilesManager.DownloadStreamAsync(SelectedItem.Id))
            using (var writeStream = await saveFile.OpenStreamForWriteAsync())
            {
                await dataStream.CopyToAsync(writeStream);
            }

            await new MessageDialog(string.Format("File Saved to: {0}", saveFile.Path)).ShowAsync();
        }

        internal async Task Upload()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add("*");
            StorageFile openFile = await fileOpenPicker.PickSingleFileAsync();
            if (openFile == null)
                return;

            var stream = await openFile.OpenStreamForReadAsync();

            BoxFileRequest fileReq = new BoxFileRequest()
            {
                Name = openFile.Name,
                Parent = new BoxRequestEntity() { Id = FolderId }
            };
            BoxFile file = await Client.FilesManager.UploadAsync(fileReq, stream);
            Items.Add(file);
        }
#endif

    }
}
