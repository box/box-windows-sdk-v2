using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using System.IO;
using Box.V2.Sample.ViewModels;
using System.Threading.Tasks;

namespace Box.V2.WP
{
    public partial class PreviewPage : PhoneApplicationPage
    {
        MainViewModel _main;
        IsolatedStorageFile _isoStore;

        private const string fileNameFormat = "{0}-{1}{2}";
        private const string addPreviewContainers = "addPreviewContainers";//('{0}', {1}, {2})";
        private const string addPreview = "addPreview";//('{0}', {1}, '{2}')";
        private const string previewExt = ".png";
        private const int buffer = 5;


        int pagesLoaded = 1;

        public PreviewPage()
        {
            InitializeComponent();
            Init();
        }

        private async Task Init()
        {
            _main = ViewModelLocator.Main;

            _isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            SaveFilesToIsoStore();

            await previewBrowser.ClearInternetCacheAsync();
            previewBrowser.Navigate(new Uri("/preview.html", UriKind.Relative));
        }

        private async Task GetNextPages(string id, int start, int num)
        {
            int length = start + num;

            previewBrowser.InvokeScript(addPreviewContainers, new string[] { id, start.ToString(), length.ToString() });

            List<Task> tasks = new List<Task>();

            for (int i = start; i < length; ++i)
            {
                Task<Stream> previewStream = _main.Client.FilesManager.GetPreviewAsync(id, i);
                tasks.Add(SavePreviewToDisk(string.Format(fileNameFormat, id, i, previewExt), previewStream));
            }

            await Task.WhenAll(tasks);
            for (int i = start; i < length; ++i)
            {
                previewBrowser.InvokeScript(addPreview, new string[] { id, i.ToString(), previewExt });
            }
        }

        private async Task GetPreview(string id, string pageNum)
        {
            Task<Stream> previewStream = _main.Client.FilesManager.GetPreviewAsync(id, int.Parse(pageNum));
            await SavePreviewToDisk(string.Format(fileNameFormat, id, pageNum, previewExt), previewStream);
        }


        private async Task SavePreviewToDisk(string fileName, Task<Stream> previewStream)
        {
            using (IsolatedStorageFileStream destStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, _isoStore))
            {
                await (await previewStream).CopyToAsync(destStream);
            }
        }

        private void SaveFilesToIsoStore()
        {
            //These files must match what is included in the application package,
            //or BinaryStream.Dispose below will throw an exception.
            string[] files = {
            "preview.html", "jquery-1.10.1.min.js", "waypoints.min.js"
        };

            foreach (string f in files)
            {
                StreamResourceInfo sr = Application.GetResourceStream(new Uri(f, UriKind.Relative));
                using (BinaryReader br = new BinaryReader(sr.Stream))
                {
                    byte[] data = br.ReadBytes((int)sr.Stream.Length);
                    SaveToIsoStore(f, data);
                }
            }
        }

        private void SaveToIsoStore(string fileName, byte[] data)
        {
            string strBaseDir = string.Empty;
            string delimStr = "/";
            char[] delimiter = delimStr.ToCharArray();
            string[] dirsPath = fileName.Split(delimiter);

            //Re-create the directory structure.
            for (int i = 0; i < dirsPath.Length - 1; i++)
            {
                strBaseDir = System.IO.Path.Combine(strBaseDir, dirsPath[i]);
                _isoStore.CreateDirectory(strBaseDir);
            }

            //Remove the existing file.
            if (_isoStore.FileExists(fileName))
            {
                _isoStore.DeleteFile(fileName);
            }

            //Write the file.
            using (BinaryWriter bw = new BinaryWriter(_isoStore.CreateFile(fileName)))
            {
                bw.Write(data);
                bw.Close();
            }
        }

        private async void previewBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            await GetNextPages("8356335198", 0, 5);
        }

        private async void previewBrowser_ScriptNotify(object sender, NotifyEventArgs e)
        {
            string pageNum = e.Value;
            await GetPreview("8356335198", pageNum);
            previewBrowser.InvokeScript("addPreview", new string[] { "8356335198", pageNum });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            GetNextPages(_main.SelectedItem.Id, pagesLoaded, buffer);
            pagesLoaded += buffer;
        }

        private async void previewBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            GetNextPages(_main.SelectedItem.Id, pagesLoaded, buffer);
            pagesLoaded += buffer;
        }

    }
}