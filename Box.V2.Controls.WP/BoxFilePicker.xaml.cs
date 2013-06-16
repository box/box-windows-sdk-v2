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

namespace Box.V2.Controls
{
    public partial class BoxFilePicker : UserControl
    {
        Action<BoxItem> ItemSelected;
        BoxItemPickerViewModel _vm;
        PhoneApplicationPage _parent;

        public BoxFilePicker()
        {
            InitializeComponent();
        }

        async void parent_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
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

        #region Dependency Properties

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(BoxFilePicker), new PropertyMetadata(null));

        public double PopupHeight
        {
            get { return (double)GetValue(PopupHeightProperty); }
            set { SetValue(PopupHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupHeightProperty =
            DependencyProperty.Register("PopupHeight", typeof(double), typeof(BoxFilePicker), new PropertyMetadata(null));

        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupWidthProperty =
            DependencyProperty.Register("PopupWidth", typeof(double), typeof(BoxFilePicker), new PropertyMetadata(null));


        public BoxClient Client
        {
            get { return (BoxClient)GetValue(ClientProperty); }
            set { SetValue(ClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Client.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClientProperty =
            DependencyProperty.Register("Client", typeof(BoxClient), typeof(BoxFilePicker), new PropertyMetadata(null));


        public int StartingFolderId
        {
            get { return (int)GetValue(StartingFolderIdProperty); }
            set { SetValue(StartingFolderIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartingFolderId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartingFolderIdProperty =
            DependencyProperty.Register("StartingFolderId", typeof(int), typeof(BoxFilePicker), new PropertyMetadata(0));

        #endregion

        #region Event Handlers

        private async void FilePicker_Click(object sender, RoutedEventArgs e)
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

        #endregion

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
