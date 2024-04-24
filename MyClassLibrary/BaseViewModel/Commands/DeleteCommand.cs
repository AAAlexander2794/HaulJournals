using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyClassLibrary.BaseViewModel.Abstract;

namespace MyClassLibrary.BaseViewModel.Commands
{
    public class DeleteCommand<TVm, T> : ICommand
    {
        private readonly BaseViewModel<TVm, T> _viewModel;

        public DeleteCommand(BaseViewModel<TVm, T> viewModel)
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
            if (_viewModel.SelectedItem == null) return;
            if (MessageBox.Show($"Действительно удалить строку?\n",
                            "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            _viewModel.Collection.Remove(_viewModel.SelectedItem);
            _viewModel.CollectionChangedInvoke();
        }
    }
}
