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
    public class BoxItemPicker : UserControl
    {
        public Action<BoxItem> ItemSelected;
        internal BoxItemPickerViewModel _vm;


#if WINDOWS_PHONE
        protected PhoneApplicationPage _parent;

        protected virtual async void parent_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Override the back key press
            e.Cancel = true;

            var parentFolder = await _vm.GetParentFolder();
            if (!string.IsNullOrWhiteSpace(parentFolder))
            {
                await _vm.GetFolderItems(parentFolder);
            }
            else
            {
                _parent.BackKeyPress -= parent_BackKeyPress;
                IsOpen = false;
            }
        }
#else
        public event EventHandler CloseRequested;
#endif



        #region Dependency Properties

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(BoxItemPicker), new PropertyMetadata(false));

        public double PopupHeight
        {
            get { return (double)GetValue(PopupHeightProperty); }
            set { SetValue(PopupHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupHeightProperty =
            DependencyProperty.Register("PopupHeight", typeof(double), typeof(BoxItemPicker), new PropertyMetadata(null));

        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupWidthProperty =
            DependencyProperty.Register("PopupWidth", typeof(double), typeof(BoxItemPicker), new PropertyMetadata(null));


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
        public BoxItemPickerType ItemPickerType
        {
            get { return (BoxItemPickerType)GetValue(ItemPickerTypeProperty); }
            set { SetValue(ItemPickerTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPickerType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPickerTypeProperty =
            DependencyProperty.Register("ItemPickerType", typeof(BoxItemPickerType), typeof(BoxItemPicker), new PropertyMetadata(BoxItemPickerType.File));

        
        #endregion
    }

    public enum BoxItemPickerType
    {
        Folder,
        File
    }
}
