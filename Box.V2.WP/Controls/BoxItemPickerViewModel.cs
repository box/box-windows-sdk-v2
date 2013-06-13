using Box.V2.Models;
using Box.V2.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Core;

namespace Box.V2.WP.Controls
{
    internal class BoxItemPickerViewModel : BaseViewModel
    {
        private const int _numItems = 20;
        private BoxClient _client;

        public BoxItemPickerViewModel(BoxClient client)
        {
            _client = client;
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

        public async Task GetFolderItems(string id)
        {
            Items.Clear();
            FolderName = string.Empty;
            int itemCount = 0;
            IsLoading = true;

            BoxFolder folder;
            do
            {
                folder = await _client.FoldersManager.GetItemsAsync(id, _numItems, itemCount);
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

                    BoxItemViewModel biVM = new BoxItemViewModel(i);

                    if (i.Type == "folder")
                        biVM.ImageUri = new Uri("Assets/PrivateFolder.png", UriKind.Relative);
                    else
                        await biVM.GetThumbnail(_client);

                    Items.Add(biVM);
                }
                itemCount += _numItems;
            } while (itemCount < folder.ItemCollection.TotalCount);
        }
    }
}
