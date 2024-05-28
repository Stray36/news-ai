using System;
using System.Windows.Input;

// 实现 ICommand 接口，将命令逻辑从视图层分离
namespace NewsApp.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public RelayCommand(Action execute) : this(execute, () => true) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }

}



