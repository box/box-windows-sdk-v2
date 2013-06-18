using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Box.V2.Models;
using System.Threading.Tasks;

namespace Box.V2.Controls
{
    public partial class BoxFilePickerPage : PhoneApplicationPage
    {
        BoxItemPickerViewModel _vm;

        public event EventHandler CloseRequested;

        public BoxFilePickerPage(BoxClient client)
        {
            InitializeComponent();
            
            _vm = new BoxItemPickerViewModel(client);
            this.DataContext = _vm;


            LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;
            LayoutRoot.Height = Application.Current.Host.Content.ActualHeight;
        }

        private async void lbItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bivm = _vm.SelectedItem as BoxItemViewModel;
            if (bivm == null)
                return;

            if (bivm.Item.Type == "folder")
                await _vm.GetFolderItems(bivm.Item.Id);
            else if (bivm.Item.Type == "file")
            {
                SelectedItem = bivm.Item;
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
            DependencyProperty.Register("SelectedItem", typeof(BoxItem), typeof(BoxFilePickerPage), new PropertyMetadata(null));

        public async Task Init(string folderId, string folderName)
        {
            await _vm.GetFolderItems(folderId, folderName);
        }
    }
}