using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Threading;

#if WINDOWS_PHONE
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
#else
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.Storage.Streams;
#endif

namespace Box.V2.Controls
{
    public class BoxItemViewModel : BaseViewModel
    {
        private BoxClient _client;

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

        private BoxItemType _itemType;

        public BoxItemType ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }


        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                if (_image == null)
                {
                    SetThumbnailAsync(Item.Id, _client);
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
            if (item.Type == "file")
            {
                Size = item.Size;
                ItemType = BoxItemType.File;
            }
            else if (item.Type == "folder")
            {
                ItemType = BoxItemType.Folder;
            }

        }

        private async Task SetThumbnailAsync(string id, BoxClient client)
        {
            var image = await GetThumbnailAsync(id, client);
            Image = image;
        }

        private async Task<BitmapImage> GetThumbnailAsync(string id, BoxClient client)
        {
            BitmapImage image = new BitmapImage();

            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream imageStream = await _client.FilesManager.GetThumbnailAsync(Item.Id, 50, 50))
                {
                    if (imageStream == null)
                        return image;

                    await imageStream.CopyToAsync(ms);
                    if (ms.Length == 0)
                        return image;
                }
#if DEBUG
                Debug.WriteLine(string.Format("Stream received: {0}", Item.Id));
#endif

#if WINDOWS_PHONE
                image.SetSource(ms);
#else
                IRandomAccessStream randStream = await ConvertToRandomAccessStream(ms);
                await image.SetSourceAsync(randStream);
#endif
                return image;
            }
        }

#if NETFX_CORE

        public async Task<IRandomAccessStream> ConvertToRandomAccessStream(MemoryStream memoryStream)
        {
            var randomAccessStream = new InMemoryRandomAccessStream();

            var outputStream = randomAccessStream.GetOutputStreamAt(0);
            var dw = new DataWriter(outputStream);
            var task = new Task(() => dw.WriteBytes(memoryStream.ToArray()));
            task.Start();

            await task;
            await dw.StoreAsync();

            await outputStream.FlushAsync();

            return randomAccessStream;
        }

#endif
    }
}
