using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Models;
using Box.V2.Services;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Box.V2.W8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string ClientId = "pweqblqwil7cpmvgu45jaokt3qw77wbo";
        public const string ClientSecret = "dTrKxu2JYDeYIyQKSKLDf57HVlWjvU10";

        // Ryan's Dev keys
        //private const string ClientId = "yrizdmqzb9jw4bf6c3cged90xyjyzlzy";
        //public const string ClientSecret = "c6vRohbuxHCn7ol6yDdho6prcQg0buRJ";

        public const string RedirectUri = "http://localhost";

        private IBoxConfig _config;
        private BoxClient _client;

        public MainPage()
        {
            this.InitializeComponent();

            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string authCode = await Authenticate();
            var session = await _client.Auth.AuthenticateAsync(authCode);

            //await TestRefreshToken();
            //await TestFolderInfo();
            //await TestCreateFolder();
            //await TestDownloadFile();
            //await TestUploadBytes();
            await TestUploadStream();
        }


        private async Task TestUploadStream()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.FileTypeFilter.Add("*");
            StorageFile openFile = await fileOpenPicker.PickSingleFileAsync();
            var stream = await openFile.OpenStreamForReadAsync();

            BoxFileRequest fileReq = new BoxFileRequest()
            {
                Name = openFile.Name,
                Parent = new BoxRequestEntity() { Id = "0" }
            };
            File file = await _client.FilesManager.UploadAsync(fileReq, stream);

        }

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

        private async Task TestDownloadFile()
        {
            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedFileName = "test.xml"; //f.Name;
            fileSavePicker.FileTypeChoices.Add("xml", new string[] { ".xml" });
            StorageFile saveFile = await fileSavePicker.PickSaveFileAsync();

            byte[] data = await _client.FilesManager.DownloadBytesAsync("7546361455");

            await Windows.Storage.FileIO.WriteBytesAsync(saveFile, data);
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
    }
}
