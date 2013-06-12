using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Box.V2.WP
{
    public partial class BoxFilePicker : UserControl
    {
        public BoxFilePicker()
        {
            InitializeComponent();
        }

        private void FilePicker_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = true;
        }



        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(BoxFilePicker), new PropertyMetadata(null));

        
    }
}
