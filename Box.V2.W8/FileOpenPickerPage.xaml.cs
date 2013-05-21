using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The File Open Picker Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234239

namespace Box.V2.W8
{
    /// <summary>
    /// This page displays files owned by the application so that the user can grant another application
    /// access to them.
    /// </summary>
    public sealed partial class FileOpenPickerPage : Box.V2.W8.Common.LayoutAwarePage
    {
        /// <summary>
        /// Files are added to or removed from the Windows UI to let Windows know what has been selected.
        /// </summary>
        private Windows.Storage.Pickers.Provider.FileOpenPickerUI _fileOpenPickerUI;

        public FileOpenPickerPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when another application wants to open files from this application.
        /// </summary>
        /// <param name="args">Activation data used to coordinate the process with Windows.</param>
        public void Activate(FileOpenPickerActivatedEventArgs args)
        {
            this._fileOpenPickerUI = args.FileOpenPickerUI;
            _fileOpenPickerUI.FileRemoved += this.FilePickerUI_FileRemoved;

            // TODO: Set this.DefaultViewModel["Files"] to show a collection of items,
            //       each of which should have bindable Image, Title, and Description

            this.DefaultViewModel["CanGoUp"] = false;
            Window.Current.Content = this;
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when user removes one of the items from the Picker basket
        /// </summary>
        /// <param name="sender">The FileOpenPickerUI instance used to contain the available files.</param>
        /// <param name="args">Event data that describes the file removed.</param>
        private void FilePickerUI_FileRemoved(Windows.Storage.Pickers.Provider.FileOpenPickerUI sender, Windows.Storage.Pickers.Provider.FileRemovedEventArgs args)
        {
            // TODO: Respond to an item being deselected in the picker UI.
        }

        /// <summary>
        /// Invoked when the selected collection of files changes.
        /// </summary>
        /// <param name="sender">The GridView instance used to display the available files.</param>
        /// <param name="e">Event data that describes how the selection changed.</param>
        private void FileGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Update Windows UI using this._fileOpenPickerUI.AddFile and
            //       this._fileOpenPickerUI.RemoveFile
        }

        /// <summary>
        /// Invoked when the "Go up" button is clicked, indicating that the user wants to pop up
        /// a level in the hierarchy of files.
        /// </summary>
        /// <param name="sender">The Button instance used to represent the "Go up" command.</param>
        /// <param name="args">Event data that describes how the button was clicked.</param>
        private void GoUpButton_Click(object sender, RoutedEventArgs args)
        {
            // TODO: Set this.DefaultViewModel["CanGoUp"] to true to enable the corresponding command,
            //       use updates to this.DefaultViewModel["Files"] to reflect file hierarchy traversal
        }
    }
}
