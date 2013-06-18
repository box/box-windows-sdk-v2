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
using Box.V2.Controls;
using System.Windows.Controls.Primitives;

namespace Box.V2.Controls
{
    public partial class BoxFilePicker : BoxItemPicker
    {
        public BoxFilePicker()
        {
            InitializeComponent();
        }

        #region Event Handlers

        Popup _pickerPopup;
        BoxFilePickerPage _pickerPage;


        protected async void itemPicker_Click(object sender, RoutedEventArgs e)
        {
            this.Client.ThrowIfNull("Client");

            _pickerPopup = new Popup();
            _pickerPage = ItemPickerType == BoxItemPickerType.File ? 
                new BoxFilePickerPage(Client) : 
                null;
            _pickerPage.CloseRequested += filePickerPage_CloseRequested;
            _pickerPopup.Child = _pickerPage;
            _pickerPopup.IsOpen = true;
            await _pickerPage.Init(StartingFolderId.ToString(), StartingFolderName);
        }

        protected void filePickerPage_CloseRequested(object sender, EventArgs e)
        {
            _pickerPopup.IsOpen = false;

            if (_pickerPage.SelectedItem != null)
                ItemSelected(_pickerPage.SelectedItem);
        }


        #endregion

        //protected async Task InitAndLaunchItemPicker()
        //{
        //    Client.ThrowIfNull("Client");

        //    // Add listener for the back key press
        //    _parent = this.GetParentOfType<PhoneApplicationPage>();
        //    _parent.BackKeyPress += parent_BackKeyPress;

        //    // Add the view model to the popup window
        //    _vm = new BoxItemPickerViewModel(Client);
        //    puItemPicker.DataContext = _vm;



        //    // Set height and width to size of screen
        //    PopupWidth = Application.Current.Host.Content.ActualWidth;
        //    PopupHeight = Application.Current.Host.Content.ActualHeight;
        //    IsOpen = true;

        //    await _vm.GetFolderItems(StartingFolderId.ToString());
        //}


    }
}
