using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyClassLibrary.BaseViewModel.Abstract;

namespace MyClassLibrary.BaseViewModel.Commands
{
    public class SaveChanges<TVm, T> : ICommand
    {
        private readonly BaseViewModel<TVm, T> _viewModel;

        public SaveChanges(BaseViewModel<TVm, T> viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //_viewModel.SaveChanges();
        }
    }
}
