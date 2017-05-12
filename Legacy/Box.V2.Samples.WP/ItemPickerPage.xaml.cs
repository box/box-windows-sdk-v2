using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Box.V2.Samples.WP
{
    public partial class ItemPickerPage : PhoneApplicationPage
    {
        public ItemPickerPage()
        {
            InitializeComponent();

            filePicker.ItemSelected += itemPicker_ItemSelected;
            folderPicker.ItemSelected += itemPicker_ItemSelected;
        }

        void itemPicker_ItemSelected(object sender, Models.BoxItem e)
        {
            if (e != null)
                MessageBox.Show(string.Format("{0} Selected!", e.Name));
        }
    }
}