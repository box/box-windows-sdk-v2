using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Box.V2.W8.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            _dispatcher = Window.Current.Dispatcher;
        }

        private CoreDispatcher _dispatcher = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private async Task UIThreadAction(Action act)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => act.Invoke());
        }

        internal async void PropertyChangedAsync(string property)
        {
            if (PropertyChanged != null)
                await UIThreadAction(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
        }
    }
}