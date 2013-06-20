using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Core;

#if NETFX_CORE
using Windows.UI.Xaml;
#endif

namespace Box.V2.Controls
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
#if NETFX_CORE
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
#if NETFX_CORE
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => act.Invoke());
#else
            Deployment.Current.Dispatcher.BeginInvoke(act);
#endif
        }

        public async void PropertyChangedAsync(string property)
        {
            if (PropertyChanged != null)
                await UIThreadAction(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
        }
    }
}