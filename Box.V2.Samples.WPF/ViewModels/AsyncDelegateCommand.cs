using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Box.V2.Samples.WPF
{
    public class AsyncDelegateCommand : ICommand
    {
        protected readonly Predicate<object> _canExecute;
        protected Func<object, Task> _asyncExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AsyncDelegateCommand(Func<object, Task> execute)
            : this(execute, null)
        {
        }

        public AsyncDelegateCommand(Func<object, Task> asyncExecute,
                       Predicate<object> canExecute)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        protected virtual async Task ExecuteAsync(object parameter)
        {
            await _asyncExecute(parameter);
        }
    }
}
