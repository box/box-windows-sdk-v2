using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Box.V2.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class BoxFilePickerPage : BoxItemPickerPage
    {
        Page _parent;

        internal BoxFilePickerPage(BoxClient client)
        {
            this.InitializeComponent();
            Init(client);
        }

        private async void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var parentFolder = await _vm.PopParentFolder();
            if (!string.IsNullOrWhiteSpace(parentFolder))
            {
                await _vm.GetFolderItems(parentFolder);
            }
            else
            {
                Close();
            }
        }

        private async void FolderView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemVM = e.ClickedItem as BoxItemViewModel;
            if (itemVM == null)
                return;

            var item = itemVM.Item;

            if (item.Type == "folder")
            {
                await _vm.GetFolderItems(item.Id, item.Name);
            }
            else if (item.Type == "file")
            {
                SelectedItem = item;
                Close();
            }
        }


        internal override void SwapAppBar(Page parent)
        {
            if (parent == null)
                return;

            _parent = parent;
            if (_parent.TopAppBar != null)
                _parent.TopAppBar.Opened += AppBar_Opened;
            if (_parent.BottomAppBar != null)
                _parent.BottomAppBar.Opened += AppBar_Opened;

        }
        
        void AppBar_Opened(object sender, object e)
        {
            var appBar = sender as AppBar;
            if (appBar == null)
                return;

            appBar.IsOpen = false;
        }

        internal override void SwapBackAppBar()
        {
            if (_parent == null)
                return;

            if (_parent.TopAppBar != null)
                _parent.TopAppBar.Opened -= AppBar_Opened;
            if (_parent.BottomAppBar != null)
                _parent.BottomAppBar.Opened -= AppBar_Opened;
        }

    }
}
