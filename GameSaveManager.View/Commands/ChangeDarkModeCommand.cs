
using System;
using System.Windows.Input;

namespace GameSaveManager.WPF.Commands
{
    public class ChangeDarkModeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

        }
    }
}
