using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Core;
using System.Threading;

#if WINDOWS_PHONE
using System.Windows.Media.Imaging;
using Nito.AsyncEx;
#else
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
#endif



namespace Box.V2.Controls
{
    internal class BoxItemPickerViewModel : BaseViewModel
    {
        private const int _numItems = 100;
        public BoxClient _client;

        SemaphoreSlim _parentFolderLock = new SemaphoreSlim(1);
        SemaphoreSlim _itemsLock = new SemaphoreSlim(1);

        private Stack<string> _parentFolders = new Stack<string>();

        public BoxItemPickerViewModel(BoxClient client)
        {
            _client = client;
        }

        private BoxFolder _currentFolder;
        public BoxFolder CurrentFolder
        {
            get { return _currentFolder; }
            set
            {
                if (_currentFolder != value)
                {
                    _currentFolder = value;
                    PropertyChangedAsync("CurrentFolder");
                    FolderName = _currentFolder.Name;
                    FolderId = _currentFolder.Id;
                }
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

        private BoxItemViewModel _selectedItem;
        public BoxItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    PropertyChangedAsync("SelectedItem");
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

        private ObservableCollection<BoxItemViewModel> _items = new ObservableCollection<BoxItemViewModel>();
        public ObservableCollection<BoxItemViewModel> Items
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

        public async Task GetFolderItems(string id, string folderName = null)
        {
            await _itemsLock.WaitAsync();
            FolderName = folderName;
            try
            {
                Items.Clear();
                FolderName = folderName;
                int itemCount = 0;
                IsLoading = true;

                BoxFolder folder;
                do
                {
                    folder = await _client.FoldersManager.GetItemsAsync(id, _numItems, itemCount,
                        new List<string>() { 
                        BoxItem.FieldName, 
                        BoxItem.FieldModifiedAt, 
                        BoxItem.FieldSize, 
                        BoxFolder.FieldItemCollection, 
                        BoxFolder.FieldPathCollection, 
                        BoxCollection.FieldTotalCount });
                    IsLoading = false;
                    if (folder == null)
                    {
                        string message = "Unable to get folder items. Please try again later";
                        break;
                    }

                    // Is first time in loop
                    if (itemCount == 0)
                    {
                        CurrentFolder = folder;

                        if (folder.PathCollection != null && folder.PathCollection.TotalCount > 0)
                        {
                            var parent = folder.PathCollection.Entries.LastOrDefault();
                            if (parent != null && parent.Id != await PeekParentFolder())
                                await PushParentFolder(parent.Id);
                        }
                    }

                    foreach (var i in folder.ItemCollection.Entries)
                    {
                        BoxItemViewModel biVM = new BoxItemViewModel(i, _client);
                        if (i.Type == "folder")
                        {
#if WINDOWS_PHONE
                        biVM.Image = new BitmapImage(new Uri("/Assets/PrivateFolder.png", UriKind.RelativeOrAbsolute));
#else
                            var uri = new System.Uri("ms-appx:///Box.V2.Controls.W8/Assets/PrivateFolder.png");
                            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
                            var stream = await file.OpenReadAsync();
                            biVM.Image = new BitmapImage();
                            await biVM.Image.SetSourceAsync(stream);
#endif
                        }
                        Items.Add(biVM);
                    }
                    itemCount += _numItems;
                } while (itemCount < folder.ItemCollection.TotalCount);
            }
            finally
            {
                _itemsLock.Release();
            }
        }

        public async Task<string> PeekParentFolder()
        {
            await _parentFolderLock.WaitAsync();
            try
            {
                if (_parentFolders.Count > 0)
                    return _parentFolders.Peek();
                return string.Empty;
            }
            finally
            {
                _parentFolderLock.Release();
            }
        }

        public async Task<string> PopParentFolder()
        {
            await _parentFolderLock.WaitAsync();
            try
            {
                if (_parentFolders.Count > 0)
                    return _parentFolders.Pop();
                return null;
            }
            finally
            {
                _parentFolderLock.Release();
            }
        }

        public async Task PushParentFolder(string folderId)
        {
            if (string.IsNullOrWhiteSpace(folderId))
                return;

            await _parentFolderLock.WaitAsync();
            try
            {
                _parentFolders.Push(folderId);
            }
            finally
            {
                _parentFolderLock.Release();
            }
        }
    }
}
