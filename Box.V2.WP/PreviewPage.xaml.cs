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

namespace Box.V2.WP
{
    public partial class PreviewPage : PhoneApplicationPage
    {
        MainViewModel _main;

        public PreviewPage()
        {
            InitializeComponent();
            _main = ViewModelLocator.Main;
        }


        private async void previewBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            SaveFilesToIsoStore();

            Stream test = await _main.Client.FilesManager.GetPreviewAsync("8356335198", 1);
            //Stream test = await _client.FilesManager.GetThumbnailAsync("8356335198");
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication();

            string fileName = "test.png";

            using (IsolatedStorageFileStream destStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoFile))
            {
                await test.CopyToAsync(destStream);
            }
            previewBrowser.Navigate(new Uri("/preview.html", UriKind.Relative));
            //string html = string.Format("<html><head><title>Sample img test</title></head><body><img src=\"{0}\"/></body></html>", fileName);
            //oauthBrowser.NavigateToString(html);

        }



        private void SaveFilesToIsoStore()
        {
            //These files must match what is included in the application package,
            //or BinaryStream.Dispose below will throw an exception.
            string[] files = {
            "preview.html", "jquery-2.0.2.min.js"
        };

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

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

            //Get the IsoStore.
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

            //Re-create the directory structure.
            for (int i = 0; i < dirsPath.Length - 1; i++)
            {
                strBaseDir = System.IO.Path.Combine(strBaseDir, dirsPath[i]);
                isoStore.CreateDirectory(strBaseDir);
            }

            //Remove the existing file.
            if (isoStore.FileExists(fileName))
            {
                isoStore.DeleteFile(fileName);
            }

            //Write the file.
            using (BinaryWriter bw = new BinaryWriter(isoStore.CreateFile(fileName)))
            {
                bw.Write(data);
                bw.Close();
            }
        }

        private void previewBrowser_Navigated(object sender, NavigationEventArgs e)
        {

            //string[] codeString = { String.Format(" {0}('{1}') ", @"$.city.venue.onVenueSelected", result.ToString()) };
            //this.myBrowser.Document.InvokeScript("eval", codeString);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            previewBrowser.InvokeScript("addPreviews");
        }

    }
}