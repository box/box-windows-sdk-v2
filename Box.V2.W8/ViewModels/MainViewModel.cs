using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using Windows.UI.Popups;

namespace Box.V2.W8.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public const string ClientId = "pweqblqwil7cpmvgu45jaokt3qw77wbo";
        public const string ClientSecret = "dTrKxu2JYDeYIyQKSKLDf57HVlWjvU10";

        // Ryan's Dev keys
        //private const string ClientId = "yrizdmqzb9jw4bf6c3cged90xyjyzlzy";
        //public const string ClientSecret = "c6vRohbuxHCn7ol6yDdho6prcQg0buRJ";

        public const string RedirectUri = "http://localhost";
        public readonly int ItemLimit = 5;

        private IBoxConfig _config;
        private BoxClient _client;

        public MainViewModel() : base() { }

        #region Properties

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items
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

        private Item _selectedItem;
        public Item SelectedItem
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

        public async void Init()
        {
            OAuthSession session = null;
            session = new OAuthSession("l3iueabbUADqNCPOJlXzthJRliSpdyFt", "DmFLf5Pb5M0DR76SIW8evarPw3qNedD7msPwD5vez1cWXu0DnjbMcJHmvMlYxie3", 3600, "bearer");

            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config, session);

            //string authCode = await Authenticate();
            //await _client.Auth.AuthenticateAsync(authCode);

            // Get the root folder
            await GetFolderItems("0", ItemLimit);

            //await TestRefreshToken();
            //await TestFolderInfo();
            //await TestCreateFolder();
            //await TestDownloadFile();
            //await TestUploadBytes();
            //await TestUploadStream();
        }

        public async Task<string> Authenticate()
        {
            WebAuthenticationResult war = await WebAuthenticationBroker.AuthenticateAsync(
                WebAuthenticationOptions.None,
                _client.Auth.AuthCodeUri,
                new Uri(_config.RedirectUri));

            switch (war.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    {
                        // grab auth code
                        var response = war.ResponseData;
                        WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(new Uri(response).Query);
                        return decoder.GetFirstValueByName("code");
                    }
                case WebAuthenticationStatus.UserCancel:
                    {
                        //log("HTTP Error returned by AuthenticateAsync() : " + war.ResponseErrorDetail.ToString());
                        break;
                    }
                default:
                case WebAuthenticationStatus.ErrorHttp:
                    //log("Error returned by AuthenticateAsync() : " + war.ResponseStatus.ToString());
                    break;
            }

            return string.Empty;
        }

        public async Task GetFolderItems(string id, int limit)
        {
            Items.Clear();
            FolderName = string.Empty;
            int itemCount = 0;
            IsLoading = true;

            Folder folder;
            do
            {
                folder = await _client.FoldersManager.GetItemsAsync(id, limit, itemCount);
                IsLoading = false;
                if (folder == null)
                {
                    await new MessageDialog("Unable to get folder items. Please try again later").ShowAsync();
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
                itemCount += limit;
            } while (itemCount < folder.ItemCollection.TotalCount);
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

            byte[] data = await _client.FilesManager.DownloadBytesAsync(SelectedItem.Id);

            await Windows.Storage.FileIO.WriteBytesAsync(saveFile, data);

            await new MessageDialog(string.Format("File Saved to: {0}", saveFile.Path)).ShowAsync();
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
            File file = await _client.FilesManager.UploadAsync(fileReq, stream);
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
            OAuthSession session1 = await _client.Auth.RefreshAccessTokenAsync(_client.Auth.Session.AccessToken);
        }

        private async Task TestFolderInfo()
        {
            File f = await _client.FilesManager.GetInformationAsync("7546361455");
        }

        private async Task TestGetFolderItems()
        {
            Folder f = await _client.FoldersManager.GetItemsAsync("0", 10);
        }

        private async Task TestCreateFolder()
        {
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Name = "testFolder",
                Parent = new BoxRequestEntity() { Id = "0" }
            };
            Folder fol = await _client.FoldersManager.CreateAsync(folderReq);
        }

        private async Task FakeUpload()
        {
            HttpClient client = new HttpClient();

            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add("*");
            StorageFile openFile = await fileOpenPicker.PickSingleFileAsync();
            var stream = await openFile.OpenStreamForReadAsync();


            BoxFileRequest fileReq = new BoxFileRequest()
            {
                Name = openFile.Name,
                Parent = new BoxRequestEntity() { Id = "0" }
            };
            BoxJsonConverter converter = new BoxJsonConverter();
            string content = converter.Serialize(fileReq);
            //byte[] oFile;
            //byte[] buffer = new byte[16 * 1024];
            //MemoryStream ms = new MemoryStream();
            //int read;
            //while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    ms.Write(buffer, 0, read);
            //}
            //oFile = ms.ToArray();

            MultipartFormDataContent multiForm = new MultipartFormDataContent();
            //multiForm.Add(new StringContent(content), "metadata");


            StreamContent fileContent = new StreamContent(stream);
            //ByteArrayContent fileContent = new ByteArrayContent(new byte[] { 0x41, 0x42, 0x43 });
            ////ByteArrayContent fileContent = new ByteArrayContent(oFile);
            //fileContent.Headers.Add("Content-Type", "application/octet-stream");

            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                FileName = string.Format("\"{0}\"", openFile.Name),
                Name = "\"file\""
            };
            multiForm.Add(fileContent, "file");

            multiForm.Add(new StringContent(content), "\"metadata\"");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer dxjIn0rSa2YiG71nsQTuL7K5D8dTKMuO");

            //var formContents = await multiForm.ReadAsStringAsync();

            var test = await client.PostAsync(_config.FilesUploadEndpointUri, multiForm); //"http://posttestserver.com/post.php?dir=example", multiForm); //
            var testString = await test.Content.ReadAsStringAsync();
        }

        #endregion

    }
}
