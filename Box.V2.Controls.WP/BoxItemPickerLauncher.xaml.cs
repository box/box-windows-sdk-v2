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
    public partial class BoxItemPickerLauncher : BoxItemPicker
    {
        protected PhoneApplicationPage _parent;

        public BoxItemPickerLauncher()
        {
            InitializeComponent();
            UpdateButtonText();
        }

        #region Event Handlers


        private void UpdateButtonText()
        {
            var type = (BoxItemPickerType)Enum.Parse(typeof(BoxItemPickerType), ItemPickerType.ToString());

            switch (type)
            {
                case BoxItemPickerType.File:
                    buttonLauncher.Content = FileSelectText;
                    break;
                case BoxItemPickerType.Folder:
                    buttonLauncher.Content = FolderSelectText;
                    break;
            }
        }

        protected async void itemPicker_Click(object sender, RoutedEventArgs e)
        {
            this.Client.ThrowIfNull("Client");

            _pickerPopup = new Popup();
            _pickerPage = ItemPickerType == BoxItemPickerType.File ? 
                new BoxFilePickerPage(Client) as BoxItemPickerPage :
                new BoxFolderPickerPage(Client) as BoxItemPickerPage;
            _pickerPage.CloseRequested += filePickerPage_CloseRequested;
            _pickerPopup.Child = _pickerPage;

            // Add listener for the back key press
            _parent = this.GetParentOfType<PhoneApplicationPage>();
            _parent.BackKeyPress += parent_BackKeyPress;

            // Hide existing appbar if present
            _pickerPage.SwapAppBar(_parent);

            _pickerPopup.IsOpen = true;
            await _pickerPage.Init(StartingFolderId.ToString(), StartingFolderName);
        }

        protected virtual async void parent_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Override the back key press
            e.Cancel = true;

            await _pickerPage.GoBack();
        }

        protected void filePickerPage_CloseRequested(object sender, EventArgs e)
        {
            _pickerPopup.IsOpen = false;

            // Remove popup bindings
            _parent.BackKeyPress -= parent_BackKeyPress;
            _pickerPage.SwapBackAppBar();

            if (ItemSelected != null)
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
