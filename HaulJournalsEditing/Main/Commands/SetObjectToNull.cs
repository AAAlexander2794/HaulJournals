using System;
using System.Windows.Input;
using HaulJournalsEditing.Main.Journals;

namespace HaulJournalsEditing.Main.Commands
{
    /// <summary>
    /// Устанавливает значение объекта равным null. 
    /// Объект выбирается по значению параметра (его текстовое имя)
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class SetObjectToNull : ICommand
    {
        private readonly MainViewModel _mainViewModel;

        public SetObjectToNull(MainViewModel viewModel)
        {
            this._mainViewModel = viewModel;
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
            var parameterString = parameter.ToString();
            switch (parameterString)
            {
                case "Vessel":
                    if (_mainViewModel.VoyagesViewModel.SelectedItem == null) return;
                    _mainViewModel.VoyagesViewModel.SelectedItem.Vessel = null;
                    break;
                case "Ground":
                    if (_mainViewModel.StationsViewModel.SelectedItem == null) return;
                    _mainViewModel.StationsViewModel.SelectedItem.Ground = null;
                    break;
                case "Square":
                    if (_mainViewModel.StationsViewModel.SelectedItem == null) return;
                    _mainViewModel.StationsViewModel.SelectedItem.Square = null;
                    break;
                case "WindDirect":
                    if (_mainViewModel.StationsViewModel.SelectedDistrict == null) return;
                    _mainViewModel.StationsViewModel.SelectedItem.WindDirect = null;
                    break;
                case "Age":
                    if (_mainViewModel.BioAnsViewModel.SelectedItem == null) return;
                    _mainViewModel.BioAnsViewModel.SelectedItem.Age = null;
                    break;
                case "GonadsStage":
                    if (_mainViewModel.BioAnsViewModel.SelectedItem == null) return;
                    _mainViewModel.BioAnsViewModel.SelectedItem.GonadsStage = null;
                    break;
            }


        }
    }
}
