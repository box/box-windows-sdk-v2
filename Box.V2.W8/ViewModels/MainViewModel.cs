using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Models;
using Box.V2.Services;
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

#if WINDOWS_PHONE
using System.Windows.Media.Imaging;
using System.Threading;
#endif

namespace Box.V2.Sample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // Keys on Live
        public const string ClientId = "pweqblqwil7cpmvgu45jaokt3qw77wbo";
        public const string ClientSecret = "dTrKxu2JYDeYIyQKSKLDf57HVlWjvU10";

        // Keys on Dev
        //public const string ClientId = "2simanymqjyz8hgnd5xzv0ayjdl5dhps";
        //public const string ClientSecret = "3BOQj9pOC2z01YhG17pCHw74fmmH9qqs";

        public const string RedirectUri = "http://localhost";
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
                    PropertyChangedAsync("SelectedItem");

                    AppBarOpened = _selectedItem != null;
                }
            }
        }

        private bool _appBarOpened;
        public bool AppBarOpened
        {
            get { return _appBarOpened; }
            set
            {
                if (_appBarOpened != value)
                {
                    _appBarOpened = value;
                    PropertyChangedAsync("AppBarOpened");
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
        #endregion

        public async Task Init(string authCode)
        {
            await Client.Auth.AuthenticateAsync(authCode);

            // Get the root folder
            await GetFolderItems("0");

            //await TestRefreshToken();
            //await TestFolderInfo();
            //await TestCreateFolder();
            //await TestDownloadFile();
            //await TestUploadBytes();
            //await TestUploadStream();
            //await TestGetComments();
        }

        public async Task GetFolderItems(string id)
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
#if W8
                    await new MessageDialog(message).ShowAsync();
#else
                    MessageBox.Show(message);
#endif
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
                    //Stream thumb = await Client.FilesManager.GetThumbnailAsync(id);
                }
                itemCount += ItemLimit;
            } while (itemCount < folder.ItemCollection.TotalCount);


            //string[] ids = { "7918928406",
            //                  "7918928976",
            //                  "7918929126",
            //                  "7918929466",
            //                  "7918930332",
            //                  "7918929914",
            //                  "7918930534",
            //                  "7918930810",
            //                  "7918931172",
            //                  "7918931736",
            //                  "7918931930",
            //                  "7918932720",
            //                  "7918932692",
            //                  "7918933326",
            //                  "7918933600",
            //                  "7918934086",
            //                  "7918934724" };

        }
        //SemaphoreSlim _throttler = new SemaphoreSlim(2);


        //private async Task TestThrottle(string id)
        //{
        //    await _throttler.WaitAsync();

        //    try
        //    {
        //        await Client.FilesManager.GetThumbnailAsync(id, 50, 50);
        //    }
        //    finally
        //    {
        //        _throttler.Release();
        //    }
        //}

        internal async Task Download()
        {
#if W8
            if (SelectedItem == null)
                await new MessageDialog("No File Selected").ShowAsync();

            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedFileName = SelectedItem.Name;
            var ext = Path.GetExtension(SelectedItem.Name);
            fileSavePicker.FileTypeChoices.Add(ext, new string[] { ext });
            StorageFile saveFile = await fileSavePicker.PickSaveFileAsync();

            using (Stream dataStream = await Client.FilesManager.DownloadStreamAsync(SelectedItem.Id))
            using (var writeStream = await saveFile.OpenStreamForWriteAsync())
            {
                await dataStream.CopyToAsync(writeStream);
            }

            await new MessageDialog(string.Format("File Saved to: {0}", saveFile.Path)).ShowAsync();
#endif
        }

        internal async Task Upload()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add("*");
            StorageFile openFile = await fileOpenPicker.PickSingleFileAsync();
            var stream = await openFile.OpenStreamForReadAsync();

            BoxFileRequest fileReq = new BoxFileRequest()
            {
                Name = openFile.Name,
                Parent = new BoxRequestEntity() { Id = FolderId }
            };
            BoxFile file = await Client.FilesManager.UploadAsync(fileReq, stream);
            Items.Add(file);
        }

        #region Test Methods

        private async Task TestUploadBytes()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add("*");
            StorageFile openFile = await fileOpenPicker.PickSingleFileAsync();
            var raStream = await openFile.OpenAsync(FileAccessMode.Read);
            var stream = raStream.GetInputStreamAt(0).AsStreamForRead();

            byte[] oFile;
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                oFile = ms.ToArray();
            }
        }

        private async Task TestRefreshToken()
        {
            OAuthSession session1 = await Client.Auth.RefreshAccessTokenAsync(Client.Auth.Session.AccessToken);
        }

        private async Task TestFolderInfo()
        {
            BoxFile f = await Client.FilesManager.GetInformationAsync("7546361455");
        }

        private async Task TestGetFolderItems()
        {
            BoxFolder f = await Client.FoldersManager.GetItemsAsync("0", 10);
        }

        private async Task TestCreateFolder()
        {
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Name = "testFolder",
                Parent = new BoxRequestEntity() { Id = "0" }
            };
            BoxFolder fol = await Client.FoldersManager.CreateAsync(folderReq);
        }

        private async Task TestGetComments()
        {
            BoxCollection<BoxComment> c = await Client.FilesManager.GetCommentsAsync("8356335198");
        }

        #endregion

    }
}
