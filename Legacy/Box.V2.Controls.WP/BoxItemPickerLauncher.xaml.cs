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
    /// <summary>
    /// An item picker launcher that is packaged with the SDK. The launcher defaults to a File picker
    /// but can be changed to a folder picker by setting the "ItemPickerType" to "Folder"
    /// </summary>
    public partial class BoxItemPickerLauncher : BoxItemPicker
    {
        protected PhoneApplicationPage _parent;

        public BoxItemPickerLauncher()
        {
            InitializeComponent();
        }

        #region Event Handlers

        protected async void itemPicker_Click(object sender, RoutedEventArgs e)
        {
            if (this.Client == null)
                throw new ArgumentNullException("Client");

            _pickerPopup = new Popup();
            _pickerPage = ItemPickerType == BoxItemType.File ? 
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

            OnItemSelected(_pickerPage.SelectedItem);
        }

        #endregion

    }
}
