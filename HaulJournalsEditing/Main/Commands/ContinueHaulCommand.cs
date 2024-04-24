using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HaulJournalsEditing.Enums;

namespace HaulJournalsEditing.Main.Commands
{
    public class ContinueHaulCommand : ICommand
    {
        private readonly Main.MainViewModel _viewModel;

        public ContinueHaulCommand(Main.MainViewModel viewModel)
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
            if (_viewModel.HaulsViewModel.SelectedItem == null) return;
            //Обновляем вариационный ряд, биологический анализ и промеры в 
            //соответствии с выбранным уловом
            _viewModel.VarSeriesViewModel.ParentItem = _viewModel.HaulsViewModel.SelectedItem;
            _viewModel.BioAnsViewModel.ParentItem = _viewModel.HaulsViewModel.SelectedItem;
            _viewModel.MeasurementsViewModel.ParentItem = _viewModel.HaulsViewModel.SelectedItem;
            //Переключаемся на вкладку анализа
            _viewModel.SelectedTabItem = TabItems.Analysis;
        }
    }
}
