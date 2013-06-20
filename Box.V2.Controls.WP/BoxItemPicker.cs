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
        public Action<BoxItem> ItemSelected;
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


        public BoxClient Client
        {
            get { return (BoxClient)GetValue(ClientProperty); }
            set { SetValue(ClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Client.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientProperty =
            DependencyProperty.Register("Client", typeof(BoxClient), typeof(BoxItemPicker), new PropertyMetadata(null));


        public int StartingFolderId
        {
            get { return (int)GetValue(StartingFolderIdProperty); }
            set { SetValue(StartingFolderIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartingFolderId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingFolderIdProperty =
            DependencyProperty.Register("StartingFolderId", typeof(int), typeof(BoxItemPicker), new PropertyMetadata(0));

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

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(BoxItemPickerLauncher), new PropertyMetadata(FileSelectText));
        #endregion

    }

    public enum BoxItemType
    {
        File,
        Folder
    }
}
