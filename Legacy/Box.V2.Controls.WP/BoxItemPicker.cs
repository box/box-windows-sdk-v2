using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#if NETFX_CORE
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
#endif

#if WINDOWS_PHONE
using Microsoft.Phone.Controls;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
#endif

namespace Box.V2.Controls
{
    public abstract class BoxItemPicker : UserControl
    {
        /// <summary>
        /// The event handler that is called when the appropriate item is selected
        /// </summary>
        public event EventHandler<BoxItem> ItemSelected;

        public Action<BoxItem> Selected;

        protected  Popup _pickerPopup;
        internal BoxItemPickerPage _pickerPage;

        protected const string FileSelectText = "Select a File";
        protected const string FolderSelectText = "Select a Folder";

        #region Dependency Properties

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(BoxItemPicker), new PropertyMetadata(false));

        /// <summary>
        /// The client the item picker will be using to make all subsequent API requests. 
        /// This must be a fully authenticated client
        /// </summary>
        public BoxClient Client
        {
            get { return (BoxClient)GetValue(ClientProperty); }
            set { SetValue(ClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Client.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientProperty =
            DependencyProperty.Register("Client", typeof(BoxClient), typeof(BoxItemPicker), new PropertyMetadata(null));

        /// <summary>
        /// The ID of the folder the item picker should open to
        /// </summary>
        public int StartingFolderId
        {
            get { return (int)GetValue(StartingFolderIdProperty); }
            set { SetValue(StartingFolderIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartingFolderId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingFolderIdProperty =
            DependencyProperty.Register("StartingFolderId", typeof(int), typeof(BoxItemPicker), new PropertyMetadata(0));

        /// <summary>
        /// Because the folder name will not be known until the first network request is made, a folder name can be provided so the folder 
        /// name will not be empty on start
        /// </summary>
        public string StartingFolderName
        {
            get { return (string)GetValue(StartingFolderNameProperty); }
            set { SetValue(StartingFolderNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartingFolderName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingFolderNameProperty =
            DependencyProperty.Register("StartingFolderName", typeof(string), typeof(BoxItemPicker), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The type of Item the picker return. Valid values are File and Folder with default being File
        /// </summary>
        public BoxItemType ItemPickerType
        {
            get { return (BoxItemType)GetValue(ItemPickerTypeProperty); }
            set { SetValue(ItemPickerTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPickerType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPickerTypeProperty =
            DependencyProperty.Register("ItemPickerType", typeof(BoxItemType), typeof(BoxItemPicker), new PropertyMetadata(BoxItemType.File, OnItemPickerTypePropertyChanged));

        private static void OnItemPickerTypePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var picker = o as BoxItemPicker;
            if (picker == null)
                return;

            switch (picker.ItemPickerType)
            {
                case BoxItemType.File:
                    picker.ButtonText = FileSelectText;
                    break;
                case BoxItemType.Folder:
                    picker.ButtonText = FolderSelectText;
                    break;
            }
        }

        /// <summary>
        ///  The text that the item picker button will display
        /// </summary>
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(BoxItemPickerLauncher), new PropertyMetadata(FileSelectText));
        #endregion


        /// <summary>
        /// Fires the ItemSelected EvenHandler passing in the selected item
        /// </summary>
        /// <param name="item"></param>
        public void OnItemSelected(BoxItem item)
        {
            if (ItemSelected != null)
                ItemSelected(this, item);
        }

    }

    /// <summary>
    /// The item type that the picker is based on
    /// </summary>
    public enum BoxItemType
    {
        File,
        Folder
    }
}
