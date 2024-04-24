using System;
using System.Windows;
using System.Windows.Input;
using HaulJournalsDAL.Entities;
using HaulJournalsEditing.Main.Journals;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main
{
    /// <summary>
    /// Финализирует выбранный рейс
    /// </summary>
    public class FinalizeVoyageCommand : ICommand
    {
        private readonly VoyagesViewModel _viewModel;

        public FinalizeVoyageCommand(VoyagesViewModel viewModel)
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
            if (_viewModel.SelectedItem.IsFinalized)
            {
                MessageBox.Show("Рейс уже финализирован.");
                return;
            }
            if (MessageBox.Show("Финализировать выбранный рейс?\n" +
                                "Финализированный рейс нельзя редактировать",
                    "", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            _viewModel.SelectedItem.IsFinalized = true;
        }
    }
}
