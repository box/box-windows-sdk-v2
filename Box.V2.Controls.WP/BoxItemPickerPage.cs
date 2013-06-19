using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#if WINDOWS_PHONE
using Microsoft.Phone.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

namespace Box.V2.Controls
{
    internal abstract class BoxItemPickerPage 
#if WINDOWS_PHONE
        : PhoneApplicationPage
#else
        : Page
#endif
    {
        internal BoxItemPickerViewModel _vm;
        public event EventHandler CloseRequested;

        public void Init(BoxClient client)
        {
            _vm = new BoxItemPickerViewModel(client);
            this.DataContext = _vm;

            // Set page to screen size
#if WINDOWS_PHONE
            LayoutWidth = Application.Current.Host.Content.ActualWidth;
            LayoutHeight = Application.Current.Host.Content.ActualHeight;
#else
            var bounds = Window.Current.Bounds;
            LayoutWidth = bounds.Width;
            LayoutHeight = bounds.Height;
#endif
        }

        internal async Task GoBack()
        {
            var parentFolder = await _vm.PopParentFolder();
            if (!string.IsNullOrWhiteSpace(parentFolder))
            {
                await _vm.GetFolderItems(parentFolder);
            }
            else
            {
                Close();
            }
        }

        #region Dependency Properties

        public BoxItem SelectedItem { get; set; }

        public async Task Init(string folderId, string folderName)
        {
            await _vm.GetFolderItems(folderId, folderName);
        }

        public double LayoutWidth
        {
            get { return (double)GetValue(LayoutWidthProperty); }
            set { SetValue(LayoutWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LayoutWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutWidthProperty =
            DependencyProperty.Register("LayoutWidth", typeof(double), typeof(BoxItemPickerPage), new PropertyMetadata(0.0));

        public double LayoutHeight
        {
            get { return (double)GetValue(LayoutHeightProperty); }
            set { SetValue(LayoutHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LayoutHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutHeightProperty =
            DependencyProperty.Register("LayoutHeight", typeof(double), typeof(BoxItemPickerPage), new PropertyMetadata(0.0));

        #endregion

        internal void Close()
        {
            CloseRequested(this, EventArgs.Empty);
        }

#if WINDOWS_PHONE
        internal abstract void SwapAppBar(PhoneApplicationPage _parent);
#else
        internal abstract void SwapAppBar(Page _parent);
#endif
        internal abstract void SwapBackAppBar();

        
    }
}
