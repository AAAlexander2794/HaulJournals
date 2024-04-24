﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HaulJournalsEditing.Enums;

namespace HaulJournalsEditing.Main.Commands
{
    public class SaveCommand : ICommand
    {
        private readonly Main.MainViewModel _viewModel;

        public SaveCommand(Main.MainViewModel viewModel)
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
            _viewModel.SaveChanges();
        }
    }
}
