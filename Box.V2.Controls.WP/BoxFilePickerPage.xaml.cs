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
    internal partial class BoxFilePickerPage : BoxItemPickerPage
    {
        IApplicationBar _appBar;

        internal BoxFilePickerPage(BoxClient client)
        {
            InitializeComponent();
            Init(client);
        }

        private async void lbItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bivm = _vm.SelectedItem as BoxItemViewModel;
            if (bivm == null)
                return;

            if (bivm.Item.Type == "folder")
                await _vm.GetFolderItems(bivm.Item.Id, bivm.Item.Name);
            else if (bivm.Item.Type == "file")
            {
                SelectedItem = bivm.Item;
                Close();
            }
        }

        internal override void SwapAppBar(PhoneApplicationPage _parent)
        {
            if (_parent.ApplicationBar != null && _parent.ApplicationBar.IsVisible)
            {
                _appBar = _parent.ApplicationBar;
                _appBar.IsVisible = false;
            }
        }

        internal override void SwapBackAppBar()
        {
            if (_appBar != null)
                _appBar.IsVisible = true;
        }
    }
}