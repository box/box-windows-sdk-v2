using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Core;

#if W8
using Windows.UI.Xaml;
#endif

namespace Box.V2.Controls
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
#if W8
            _dispatcher = Window.Current.Dispatcher;
#endif
        }

        private CoreDispatcher _dispatcher = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private async Task UIThreadAction(Action act)
        {
#if W8
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => act.Invoke());
#else
            Deployment.Current.Dispatcher.BeginInvoke(act);
#endif
        }

        internal async void PropertyChangedAsync(string property)
        {
            if (PropertyChanged != null)
                await UIThreadAction(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
        }
    }
}