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
using Box.V2.WP.Controls;
using System.Threading.Tasks;

namespace Box.V2.WP
{
    public partial class BoxFilePicker : UserControl
    {
        Action<BoxItem> ItemSelected;
        BoxItemPickerViewModel _vm;

        public BoxFilePicker()
        {
            InitializeComponent();
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
            Client.ThrowIfNull("Client");

            await InitAndLaunchItemPicker();
        }

        private void lbItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        protected async Task InitAndLaunchItemPicker()
        {
            _vm = new BoxItemPickerViewModel(Client);
            this.DataContext = _vm;

            // Set height and width to size of screen
            PopupWidth = Application.Current.Host.Content.ActualWidth;
            PopupHeight = Application.Current.Host.Content.ActualHeight;
            IsOpen = true;

            await _vm.GetFolderItems(StartingFolderId.ToString());
        }

    }
}
