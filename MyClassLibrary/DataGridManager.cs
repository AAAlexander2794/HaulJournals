using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

//using System.Windows.Controls.

namespace MyClassLibrary
{
    /// <summary>
    /// Содержит различные методы для программной навигации по датагриду,
    /// например получение ячейки по ее индексу,
    /// а также методы обработки нажатий клавиш для датагрида 
    /// </summary>
    public class DataGridManager
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
        public static void MyKeyHandle(object sender, KeyEventArgs e, ICommand command = null, object commandParameter = null)
        {
            //Если sender не DataGrid, то return
            if (!(sender is DataGrid dataGrid)) return;
            var u = e.OriginalSource as UIElement;

            if (e.Key == Key.Add)
            {
                e.Handled = true;
                //
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

        //public static DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        //{
        //    if (rowContainer != null)
        //    {
        //        DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
        //        if (presenter == null)
        //        {
        //            /* if the row has been virtualized away, call its ApplyTemplate() method
        //             * to build its visual tree in order for the DataGridCellsPresenter
        //             * and the DataGridCells to be created */
        //            rowContainer.ApplyTemplate();
        //            presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
        //        }
        //        if (presenter != null)
        //        {
        //            DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
        //            if (cell == null)
        //            {
        //                /* bring the column into view
        //                 * in case it has been virtualized away */
        //                dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
        //                cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
        //            }
        //            return cell;
        //        }
        //    }
        //    return null;
        //}

        //public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        //{
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //    {
        //        DependencyObject child = VisualTreeHelper.GetChild(obj, i);
        //        if (child != null && child is T)
        //            return (T)child;
        //        else
        //        {
        //            T childOfChild = FindVisualChild<T>(child);
        //            if (childOfChild != null)
        //                return childOfChild;
        //        }
        //    }
        //    return null;
        //}

        //public static void SelectCellByIndex(DataGrid dataGrid, int rowIndex, int columnIndex)
        //{
        //    if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.Cell))
        //        throw new ArgumentException("The SelectionUnit of the DataGrid must be set to Cell.");

        //    if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
        //        throw new ArgumentException(string.Format("{0} is an invalid row index.", rowIndex));

        //    if (columnIndex < 0 || columnIndex > (dataGrid.Columns.Count - 1))
        //        throw new ArgumentException(string.Format("{0} is an invalid column index.", columnIndex));

        //    dataGrid.SelectedCells.Clear();

        //    object item = dataGrid.Items[rowIndex]; //=Product X
        //    DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
        //    if (row == null)
        //    {
        //        dataGrid.ScrollIntoView(item);
        //        row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
        //    }
        //    if (row != null)
        //    {
        //        DataGridCell cell = GetCell(dataGrid, row, columnIndex);
        //        if (cell != null)
        //        {
        //            DataGridCellInfo dataGridCellInfo = new DataGridCellInfo(cell);
        //            dataGrid.SelectedCells.Add(dataGridCellInfo);
        //            cell.Focus();
        //        }
        //    }
        //}

        //public static void SelectRowByIndexes(DataGrid dataGrid, params int[] rowIndexes)
        //{
        //    if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow))
        //        throw new ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.");

        //    if (!dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended))
        //        throw new ArgumentException("The SelectionMode of the DataGrid must be set to Extended.");

        //    if (rowIndexes.Length.Equals(0) || rowIndexes.Length > dataGrid.Items.Count)
        //        throw new ArgumentException("Invalid number of indexes.");

        //    dataGrid.SelectedItems.Clear();
        //    foreach (int rowIndex in rowIndexes)
        //    {
        //        if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
        //            throw new ArgumentException(string.Format("{0} is an invalid row index.", rowIndex));

        //        object item = dataGrid.Items[rowIndex]; //=Product X
        //        dataGrid.SelectedItems.Add(item);

        //        DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
        //        if (row == null)
        //        {
        //            dataGrid.ScrollIntoView(item);
        //            row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
        //        }
        //        if (row != null)
        //        {
        //            DataGridCell cell = GetCell(dataGrid, row, 0);
        //            if (cell != null)
        //                cell.Focus();
        //        }
        //    }
        //}
    }
}
