using System;
using System.Windows.Input;
namespace BatchRename.ViewModel.Command
{
    /// <summary>
    /// Implements <see cref="ICommand"/> interface which returns <see langword="true"/> in <see cref="CanExecute(object)"/> and does nothing in <see cref="Execute(object)"/>, this class is <see langword="abstract"/>
    /// </summary>
    public class Command : ICommand
    {
        public virtual event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            return;
        }
    }
}
