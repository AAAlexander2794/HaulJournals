using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using HaulJournalsDAL.Concrete;
using HaulJournalsDAL.Entities;
using HaulJournalsEditing.Main;
using HaulJournalsUI.Concrete;
using MyClassLibrary;

namespace HaulJournalsUI.HaulJournalsEditingViews
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Main: Window
    {
        public readonly MainViewModel _mainViewModel;

        public Main()
        {
            InitializeComponent();
            var splashScreen = new SplashScreen(@"Content\SplashScreen.tif");
            splashScreen.Show(false);
            
            //Создание модели-представления для авторизованного пользователя
            //try
            //{
                _mainViewModel = new MainViewModel();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(ExceptionMessagesHandler.GetMessages(e));
            //    Environment.Exit(1);
            //}
            
            splashScreen.Close(TimeSpan.Zero);
            //Форма авторизации
            Login login = new Login();
            login.ShowDialog();
            //Помещаем модель-представление в контекст данных окна
            this.DataContext = _mainViewModel;

            //Подписка на событие для фильтрации записей в таблице

            #region События фильтрации

            //Рейсы

            _mainViewModel.VoyagesViewModel.CollectionChanged += () =>
            {
                ((CollectionViewSource) TryFindResource("VoyagesCollection")).View.Refresh();
            };

            _mainViewModel.VoyagesViewModel.SelectedVesselTypeChanged += () =>
            {
                ((CollectionViewSource) TryFindResource("VoyagesVesselsCollection")).View.Refresh();
            };

            //Станции

            _mainViewModel.StationsViewModel.CollectionChanged += () =>
            {
                ((CollectionViewSource)TryFindResource("StationsCollection")).View.Refresh();
            };

            _mainViewModel.StationsViewModel.SelectedPartitionVariantChanged += () =>
            {
                ((CollectionViewSource)TryFindResource("StationsDistrictsCollection")).View.Refresh();
            };

            _mainViewModel.StationsViewModel.SquareHaveToBeFiltered += () =>
            {
                ((CollectionViewSource)TryFindResource("StationsSquaresCollection")).View.Refresh();
            };

            //Уловы

            _mainViewModel.HaulsViewModel.CollectionChanged += () =>
            {
                ((CollectionViewSource)TryFindResource("HaulsCollection")).View.Refresh();
            };

            //Вариационные ряды

            _mainViewModel.VarSeriesViewModel.CollectionChanged += () =>
            {
                ((CollectionViewSource)TryFindResource("VarSeriesCollection")).View.Refresh();
            };

            //Биологические анализы

            _mainViewModel.BioAnsViewModel.CollectionChanged += () =>
            {
                ((CollectionViewSource)TryFindResource("BioAnsCollection")).View.Refresh();
            };

            //Промеры

            _mainViewModel.MeasurementsViewModel.CollectionChanged += () =>
            {
                ((CollectionViewSource)TryFindResource("MeasurementsCollection")).View.Refresh();
            };

            #endregion
        }

        #region Выбор фильтров коллекций
        
        //Рейсы

        private void VoyagesVesselsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.VoyagesViewModel.FilterVessels(sender, e);
        }

        private void VoyagesCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.VoyagesViewModel.FilterCollection(sender, e);
        }

        //Станции

        private void StationsDistrictsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.StationsViewModel.FilterDistricts(sender, e);
        }

        private void StationsSquaresCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.StationsViewModel.FilterSquares(sender, e);
        }

        private void StationsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.StationsViewModel.FilterCollection(sender, e);
        }

        //Уловы

        private void HaulsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.HaulsViewModel.FilterCollection(sender, e);
        }

        //Вариационные ряды

        private void VarSeriesCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.VarSeriesViewModel.FilterCollection(sender, e);
        }

        //Биологические анализы

        private void BioAnsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.BioAnsViewModel.FilterCollection(sender, e);
        }

        //Промеры

        private void MeasurementsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            _mainViewModel.MeasurementsViewModel.FilterCollection(sender, e);
        }

        #endregion

        /// <summary>
        /// Запускает авторизацию
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartLogin(object sender, RoutedEventArgs e)
        {
            //Форма авторизации
            Login login = new Login();
            login.ShowDialog();
        }

        #region Настройка клавиш для таблиц


        /// <summary>
        /// Уловы.
        /// Handles the PreviewKeyDown event of the HaulsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void HaulsDataGrid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            DataGridEventHandler.HaulsKeyDownPreviewHandle(sender, e, _mainViewModel.HaulsViewModel.Commands.AddEmptyItem);
        }

        /// <summary>
        /// Вариационный ряд.
        /// Handles the OnPreviewKeyDown event of the VarSeriesDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void VarSeriesDataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            DataGridEventHandler.VarSeriesKeyDownPreviewHandle(sender, e,
                //Передаем код рыбы, чтобы относительно его сделать логику
                _mainViewModel.HaulsViewModel.SelectedItem.FishId,
                _mainViewModel.VarSeriesViewModel.Commands.AddEmptyItem);
        }

        /// <summary>
        /// Биологический анализ.
        /// Handles the OnPreviewKeyDown event of the BioAnDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void BioAnDataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridEventHandler.StandartKeyDownPreviewHandle(sender, e, _mainViewModel.BioAnsViewModel.Commands.AddEmptyItem);
        }

        /// <summary>
        /// Промеры.
        /// Handles the OnPreviewKeyDown event of the MeasurementsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void MeasurementsDataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridEventHandler.StandartKeyDownPreviewHandle(sender, e, _mainViewModel.MeasurementsViewModel.Commands.AddEmptyItem);
        }

        #endregion


        private void VesselsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
        }

        private void VesselsDataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridEventHandler.StandartKeyDownPreviewHandle(sender, e, _mainViewModel.VesselsViewModel.Commands.AddEmptyItem);
        }
    }
}
