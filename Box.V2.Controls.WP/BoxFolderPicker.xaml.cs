using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;

namespace Box.V2.Controls
{
    public partial class BoxFolderPicker : BoxItemPicker
    {
        public BoxFolderPicker()
        {
            InitializeComponent();
        }

        protected async void ItemPicker_Click(object sender, RoutedEventArgs e)
        {
            await InitAndLaunchItemPicker();
        }


        private async void lbItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bivm = _vm.SelectedItem as BoxItemViewModel;
            if (bivm == null)
                return;

            if (bivm.Item.Type == "folder")
                await _vm.GetFolderItems(bivm.Item.Id);
            else if (bivm.Item.Type == "file")
                if (ItemSelected != null)
                    ItemSelected(bivm.Item);
        }

        protected async Task InitAndLaunchItemPicker()
        {
            Client.ThrowIfNull("Client");

            // Add listener for the back key press
            _parent = this.GetParentOfType<PhoneApplicationPage>();
            _parent.BackKeyPress += parent_BackKeyPress;

            // Add the view model to the popup window
            _vm = new BoxItemPickerViewModel(Client);
            puItemPicker.DataContext = _vm;

            // Set height and width to size of screen
            PopupWidth = Application.Current.Host.Content.ActualWidth;
            PopupHeight = Application.Current.Host.Content.ActualHeight;
            IsOpen = true;

            await _vm.GetFolderItems(StartingFolderId.ToString());
        }
    }
}
