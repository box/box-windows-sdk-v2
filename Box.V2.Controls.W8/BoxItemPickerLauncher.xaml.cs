using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Box.V2.Controls
{
    public sealed partial class BoxItemPickerLauncher : BoxItemPicker
    {
        public BoxItemPickerLauncher()
        {
            this.InitializeComponent();
        }

        Popup _pickerPopup;
        //BoxItemPickerPage _pickerPage;

        private async void itemPicker_Click(object sender, RoutedEventArgs e)
        {
            this.Client.ThrowIfNull("Client");

            _pickerPopup = new Popup();
            _pickerPage = ItemPickerType == BoxItemPickerType.File ?
                new BoxFilePickerPage(Client) as BoxItemPickerPage :
                null;
                //new BoxFolderPickerPage(Client) as BoxItemPickerPage;
            _pickerPage.CloseRequested += filePickerPage_CloseRequested;
            _pickerPopup.Child = _pickerPage;

            var frame = (Frame)Window.Current.Content;
            var page = (Page)frame.Content;
            _pickerPage.SwapAppBar(page);

            _pickerPopup.IsOpen = true;
            await _pickerPage.Init(StartingFolderId.ToString(), StartingFolderName);
        }

        private void filePickerPage_CloseRequested(object sender, EventArgs e)
        {
            _pickerPopup.IsOpen = false;
            _pickerPage.SwapBackAppBar();

            if (ItemSelected != null)
                ItemSelected(_pickerPage.SelectedItem);
        }

        
    }
}
