using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace Box.V2.Controls
{
    public partial class ImageAsync : UserControl
    {
        public ImageAsync()
        {
            InitializeComponent();
        }

        public BitmapImage Image
        {
            get { return (BitmapImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(BitmapImage), typeof(ImageAsync), new PropertyMetadata(null));

        public BitmapImage DefaultImage
        {
            get { return (BitmapImage)GetValue(DefaultImageProperty); }
            set { SetValue(DefaultImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultImageProperty =
            DependencyProperty.Register("DefaultImage", typeof(BitmapImage), typeof(ImageAsync), new PropertyMetadata(null, DefaultImagePropertyChanged));

        protected static void DefaultImagePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ImageAsync imgAsync = sender as ImageAsync;
            if (imgAsync == null || imgAsync.Image == null)
                return;

            imgAsync.imgAsync.Visibility = Visibility.Visible;
        }

    }
}
