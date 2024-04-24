using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HaulJournalsEditing.Enums;

namespace HaulJournalsEditing.Main.Commands
{
    public class ContinueStationCommand : ICommand
    {
        private readonly Main.MainViewModel _viewModel;

        public ContinueStationCommand(Main.MainViewModel viewModel)
        {
            this._viewModel = viewModel;
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
            if (_viewModel.StationsViewModel.SelectedItem == null) return;
            //Обновляем уловы в соответствии с выбранной станцией
            _viewModel.HaulsViewModel.ParentItem = _viewModel.StationsViewModel.SelectedItem;
            //Переключаемся на вкладку уловов
            _viewModel.SelectedTabItem = TabItems.Haul;
        }
    }
}
