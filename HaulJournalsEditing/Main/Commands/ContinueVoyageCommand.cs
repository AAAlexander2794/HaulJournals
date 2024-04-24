using System;
using System.Windows.Input;
using HaulJournalsEditing.Enums;

namespace HaulJournalsEditing.Main.Commands
{
    public class ContinueVoyageCommand : ICommand
    {
        private readonly Main.MainViewModel _viewModel;

        public ContinueVoyageCommand(Main.MainViewModel viewModel)
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
            if (_viewModel.VoyagesViewModel.SelectedItem == null) return;
            //Обновляем станции в соответствии с выбранным рейсом
            _viewModel.StationsViewModel.ParentItem= _viewModel.VoyagesViewModel.SelectedItem;
            //Переключаемся на вкладку станций
            _viewModel.SelectedTabItem = TabItems.Station;
            
        }
    }
}
