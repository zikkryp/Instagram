using System;
using System.Windows.Input;

namespace Instagram.DAL.Command
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
