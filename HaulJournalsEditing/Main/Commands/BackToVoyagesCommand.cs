using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HaulJournalsEditing.Enums;

namespace HaulJournalsEditing.Main.Commands
{
    public class BackToVoyagesCommand : ICommand
    {
        private readonly Main.MainViewModel _viewModel;

        public BackToVoyagesCommand(Main.MainViewModel viewModel)
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
            //
            _viewModel.VarSeriesViewModel.ParentItem = null;
            _viewModel.BioAnsViewModel.ParentItem = null;
            _viewModel.MeasurementsViewModel.ParentItem = null;
            //
            _viewModel.HaulsViewModel.ParentItem = null;
            //
            _viewModel.StationsViewModel.ParentItem = null;
            //
            _viewModel.SelectedTabItem = TabItems.Voyage;
        }
    }
}
