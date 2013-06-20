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
using Windows.Storage;

namespace Box.V2.Controls
{
    internal partial class BoxFolderPickerPage : BoxItemPickerPage
    {
        protected PhoneApplicationPage _parent;
        IApplicationBar _parentAppBar;

        internal BoxFolderPickerPage(BoxClient client)
        {
            InitializeComponent();
            Init(client);
        }

        private async void lbItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bivm = _vm.SelectedItem as BoxItemViewModel;
            if (bivm == null)
                return;

            if (bivm.Item.Type == "folder")
                await _vm.GetFolderItems(bivm.Item.Id, bivm.Item.Name);
        }

        private void Selected_Click(object sender, EventArgs e)
        {
            SelectedItem = _vm.CurrentFolder;
            Close();
        }

        internal override void SwapAppBar(PhoneApplicationPage parent)
        {
            if (parent == null)
                return; 

            _parent = parent;
            _parentAppBar = _parent.ApplicationBar;

            ApplicationBar appBar = new ApplicationBar();
            ApplicationBarIconButton button = new ApplicationBarIconButton(new Uri("/Assets/ApplicationBar.Check.png", UriKind.RelativeOrAbsolute)) { Text = "Select Folder" };
            button.Click += Selected_Click;
            appBar.Buttons.Add(button);

            _parent.ApplicationBar = appBar;

        }

        internal override void SwapBackAppBar()
        {
            _parent.ApplicationBar = _parentAppBar;
        }
    }
}