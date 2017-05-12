using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Box.V2.Samples.WPF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private async Task UIThreadAction(Action act)
        {
            await Application.Current.Dispatcher.BeginInvoke(act);
        }

        public async void PropertyChangedAsync(string property)
        {
            if (PropertyChanged != null)
                await UIThreadAction(() => PropertyChanged(this, new PropertyChangedEventArgs(property)));
        }
    }
}
