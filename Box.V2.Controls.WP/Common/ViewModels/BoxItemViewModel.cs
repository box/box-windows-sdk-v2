using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Threading;

namespace Box.V2.Controls
{
    public class BoxItemViewModel : BaseViewModel
    {
        private BoxClient _client;

        private const string fileNamePattern = "/{0}.png";
        //private readonly AsyncLock _mutex = new AsyncLock();

        public BoxItemViewModel(BoxItem item, BoxClient client)
        {
            _client = client;
            UpdateBaseBindings(item);
        }

        private BoxItem _item;
        public BoxItem Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    PropertyChangedAsync("Item");
                }
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChangedAsync("Name");
                }
            }
        }

        private DateTime? _modifiedAt;
        public DateTime? ModifiedAt
        {
            get { return _modifiedAt; }
            set 
            {
                if (_modifiedAt != value)
                {
                    _modifiedAt = value;
                    PropertyChangedAsync("ModifiedAt");
                }
            }
        }

        private long? _size;
        public long? Size
        {
            get { return _size; }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    PropertyChangedAsync("Size");
                }
            }
        }

        private Uri _imageUri;
        public Uri ImageUri
        {
            get { return _imageUri; }
            set
            {
                if (_imageUri != value)
                {
                    _imageUri = value;
                    PropertyChangedAsync("ImageUri");
                }
            }
        }

        private int _numItems;
        public int NumItems
        {
            get { return _numItems; }
            set
            {
                if (_numItems != value)
                {
                    _numItems = value;
                    PropertyChangedAsync("NumItems");
                }
            }
        }


        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    PropertyChangedAsync("ImagePath");
                }
            }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                if (_image == null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(async () =>
                    {
                        try
                        {
                            Image = await GetThumbnail(Item.Id, _client);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            Image = new BitmapImage();
                        }
                    });
                }
                return _image;
            }

            set
            {
                if (_image != value)
                {
                    _image = value;
                    PropertyChangedAsync("Image");
                }
            }
        }

        private void UpdateBaseBindings(BoxItem item)
        {
            Item = item;
            Name = item.Name;
            ModifiedAt = item.ModifiedAt;
        }

        private void UpdateFileBindings(BoxFile file)
        {
            if (file == null)
                return;

            UpdateBaseBindings(file);
            Size = file.Size;
        }

        private void UpdateFolderBindings(BoxFolder folder)
        {
            if (folder == null)
                return;

            UpdateBaseBindings(folder);
            NumItems = folder.ItemCollection.TotalCount;
        }

        public async Task GetMetadata()
        {
            SemaphoreSlim sem = new SemaphoreSlim(5);
            switch (Item.Type)
            {
                case "folder":
                    Image = new BitmapImage(new Uri("/Assets/PrivateFolder.png", UriKind.RelativeOrAbsolute));
                    UpdateFolderBindings(await _client.FoldersManager.GetInformationAsync(Item.Id, 
                        new List<string>() {
                            BoxFolder.FieldName,
                            BoxFolder.FieldModifiedAt,
                            BoxFolder.FieldItemCollection}));
                    break;
                case "file":
                    UpdateFileBindings(await _client.FilesManager.GetInformationAsync(Item.Id, 
                        new List<string>() { 
                            BoxFile.FieldName, 
                            BoxFile.FieldModifiedAt, 
                            BoxFile.FieldSize}));
                    break;
            }
        }

        public async Task<BitmapImage> GetThumbnail(string id, BoxClient client)
        {
            if (string.IsNullOrWhiteSpace(Item.Id))
                return new BitmapImage();

            Stream imageStream = await _client.FilesManager.GetThumbnailAsync(Item.Id, 50, 50);
            Debug.WriteLine(string.Format("Stream received: {0}", Item.Id));

            BitmapImage image = new BitmapImage();
            if (imageStream != null && imageStream.Length > 0)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    image.SetSource(imageStream);
                });
            }
            
            return image;

            //using (await _mutex.LockAsync())
            //{
            //    IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            //    string fileName = string.Format(fileNamePattern, id);
            //    using (IsolatedStorageFileStream destStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoStore))
            //    {
            //        await imageStream.CopyToAsync(destStream);
            //        Debug.WriteLine(string.Format("Thumbnail written: {0}", id));
            //    }

            //    if (!isoStore.FileExists(fileName))
            //        return new BitmapImage();

            //    BitmapImage bitMapImage = new BitmapImage();
            //    using (IsolatedStorageFileStream fileStream = isoStore.OpenFile(fileName, FileMode.Open, FileAccess.Read))
            //    {
            //        bitMapImage.SetSource(fileStream);
            //        Debug.WriteLine(string.Format("Thumbnail read: {0}", id));
            //        return bitMapImage;
            //    }
            //}

        }
    }
}
