using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MyClassLibrary.BaseViewModel.Abstract;
using MyClassLibrary.BaseViewModel.Enums;

namespace MyClassLibrary.BaseViewModel.Commands
{
    public class AddEmptyCommand<TVm, T> : ICommand
    {
        private readonly BaseViewModel<TVm, T> _viewModel;

        public AddEmptyCommand(BaseViewModel<TVm, T> viewModel)
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
            //Если команда вызвалась в датагриде, то необходимо закрыть редактирование ячейки
            if (parameter is DataGrid dataGrid)
            {
                dataGrid.CommitEdit();
                dataGrid.CancelEdit();
            }
            //Создаем новый элемент
            _viewModel.CreateNewItem();
            //Добавляем новый элемент в коллекцию
            _viewModel.Collection.Add(_viewModel.NewItem);
            //Делаем новый элемент выбранным
            _viewModel.SelectedItem = _viewModel.NewItem;
            //Сообщаем, что коллекция изменилась
            _viewModel.CollectionChangedInvoke();
            //Обнуляем свойство новых элементов, эл-т уже в коллекции
            _viewModel.NewItem = default(TVm);
        }
    }
}
