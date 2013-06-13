using Box.V2.Models;
using Box.V2.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.WP.Controls
{
    public class BoxItemViewModel : BaseViewModel
    {
        private const string fileNamePattern = "{0}.png";

        public BoxItemViewModel(BoxItem item)
        {
            Item = item;
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

        public async Task GetThumbnail(BoxClient client)
        {
            if (string.IsNullOrWhiteSpace(Item.Id))
                return;

            Stream imageStream = await client.FilesManager.GetThumbnailAsync(Item.Id);

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            string fileName = string.Format(fileNamePattern, Item.Id);
            using (IsolatedStorageFileStream destStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isoStore))
            {
                await imageStream.CopyToAsync(destStream);
            }

            ImageUri = new Uri(fileName, UriKind.Relative);
        }
    }
}
