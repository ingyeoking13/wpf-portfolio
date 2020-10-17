using System;
using System.Windows.Input;

namespace WPF_PortFolio.Utils
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> _execute = null;
        private readonly Predicate<object> _canExecute = null;

        public RelayCommand(Action<object> _action)
        {
            _execute = _action;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

    }
}
