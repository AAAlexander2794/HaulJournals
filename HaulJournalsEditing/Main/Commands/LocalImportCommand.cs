using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyClassLibrary;

namespace HaulJournalsEditing.Main.Commands
{
    public class LocalImportCommand : ICommand
    {
        private readonly Main.MainViewModel _viewModel;

        public LocalImportCommand(Main.MainViewModel viewModel)
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
            try
            {
                _viewModel.RefreshRepositoryLocalDirectories();
                AutoClosingMessageBox.Show("Справочники импортированы", "", 1500);
            }
            catch (Exception e)
            {
                MessageBox.Show(MyClassLibrary.ExceptionMessagesHandler.GetMessages(e));
            }
        }
    }
}
