using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HaulJournalsUI.Concrete
{
    public static class DataGridEventHandler
    {
        /// <summary>
        /// Стандартный обработчик нажатия клавиши в таблице.
        /// "+" - новая запись с переходом на нее;
        /// "Enter" - подтверждение редактирования и переход вправо
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="command">The command.</param>
        /// <param name="commandParameter">The command parameter.</param>
        public static void StandartKeyDownPreviewHandle(object sender, KeyEventArgs e, ICommand command = null,
            object commandParameter = null)
        {
            //Если sender не DataGrid, то return
            if (!(sender is DataGrid dataGrid)) return;
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Add)
            {
                e.Handled = true;
                //Закрываем и подтверждаем редактирование ячейки, чтобы данные в ней сохранились
                dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                //Выполнение какой-нибудь команды (предполагается команда на добавление сущности в таблицу)
                command?.Execute(commandParameter);

                dataGrid.SelectedItem = dataGrid.Items[dataGrid.Items.Count - 1];
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                dataGrid.CurrentCell = dataGrid.SelectedCells[0];
                dataGrid.BeginEdit();
            }

            if (e.Key == Key.Enter && u != null)
            {
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
            }
        }

        /// <summary>
        /// Обработчик нажатия клавиш для таблицы уловов.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="command">The command.</param>
        /// <param name="commandParameter">The command parameter.</param>
        public static void HaulsKeyDownPreviewHandle(object sender, KeyEventArgs e, ICommand command = null,
            object commandParameter = null)
        {
            //Если sender не DataGrid, то return
            if (!(sender is DataGrid dataGrid)) return;
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Add)
            {
                e.Handled = true;
                //Закрываем и подтверждаем редактирование ячейки, чтобы данные в ней сохранились
                dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                //Выполнение какой-нибудь команды (предполагается команда на добавление сущности в таблицу)
                command?.Execute(commandParameter);

                dataGrid.SelectedItem = dataGrid.Items[dataGrid.Items.Count - 1];
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                //Переходим сразу к коду рыбы
                dataGrid.CurrentCell = dataGrid.SelectedCells[1];
                dataGrid.BeginEdit();
            }

            if (e.Key == Key.Enter && u != null)
            {
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
            }
        }

        public static void VarSeriesKeyDownPreviewHandle(object sender, KeyEventArgs e, int fishId = 0, ICommand command = null,
            object commandParameter = null)
        {
            //Список ID осетровых типов рыб
            int[] sturgeonIdsList = new[] {1, 2, 3, 4, 5, 6, 7, 8};
            //Список ID типов рыб отнесенных к камбале (хотя здесь по-любому есть и другие)
            int[] flounderIdsList = new[] {69, 98, 129, 40, 48, 59, 42, 50, 58, 44, 43, 121, 49, 120};

            //Если sender не DataGrid, то return
            if (!(sender is DataGrid dataGrid)) return;
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Add)
            {
                e.Handled = true;
                //Закрываем и подтверждаем редактирование ячейки, чтобы данные в ней сохранились
                dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                //Выполнение какой-нибудь команды (предполагается команда на добавление сущности в таблицу)
                command?.Execute(commandParameter);

                dataGrid.SelectedItem = dataGrid.Items[dataGrid.Items.Count - 1];
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                dataGrid.CurrentCell = dataGrid.SelectedCells[0];
                dataGrid.BeginEdit();
            }

            if (e.Key == Key.Enter && u != null)
            {
                
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                //При последовательном вводе эти поля пропускаем
                if (dataGrid.CurrentColumn.Header.ToString() == "Длина 2 (мм)" && !sturgeonIdsList.Contains(fishId))
                {
                    dataGrid.CurrentColumn = dataGrid.Columns[dataGrid.CurrentColumn.DisplayIndex + 1];
                }
                if (dataGrid.CurrentColumn.Header.ToString() == "Пол (ж/м/? - 1/2/0)" && !flounderIdsList.Contains(fishId))
                {
                    dataGrid.CurrentColumn = dataGrid.Columns[dataGrid.CurrentColumn.DisplayIndex + 1];
                }
            }
        }

    }
}
