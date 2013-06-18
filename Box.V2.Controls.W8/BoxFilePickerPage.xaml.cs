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
    public sealed partial class BoxFilePickerPage : Page
    {
        BoxItemPickerViewModel _vm;
        public event EventHandler CloseRequested;

        public BoxFilePickerPage(BoxClient client)
        {
            this.InitializeComponent();

            _vm = new BoxItemPickerViewModel(client);
            this.DataContext = _vm;

            var bounds = Window.Current.Bounds;
            layoutRoot.Width = bounds.Width;
            layoutRoot.Height = bounds.Height;

        }

        private async void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var parentFolder = await _vm.GetParentFolder();
            if (!string.IsNullOrWhiteSpace(parentFolder))
            {
                await _vm.GetFolderItems(parentFolder);
            }
            else
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private async void FolderView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as BoxItem;
            if (item.Type == "folder")
            {
                await _vm.GetFolderItems(item.Id);
            }
            else if (item.Type == "file")
            {
                SelectedItem = item;
                CloseRequested(this, EventArgs.Empty);
            }
        }


        public BoxItem SelectedItem
        {
            get { return (BoxItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(BoxItem), typeof(BoxItemPicker), new PropertyMetadata(null));

        public async Task Init(string folderId, string folderName)
        {
            await _vm.GetFolderItems(folderId, folderName);
        }

    }
}
