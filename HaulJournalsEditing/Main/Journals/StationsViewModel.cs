using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using HaulJournalsDAL.Entities;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Journals
{
    public sealed class StationsViewModel : BaseViewModel<Stations, Voyages>
    {
        /// <summary>
        /// Фильтр станций для показа пользователю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is Stations item;
            //&& Collection.Contains(item)
            //&& ParentItem?.Stations != null
            //&& ParentItem.Stations.Contains(item);
        }
        
        #region Квадрат

        private ObservableCollection<PartitionVariants> _partitionVariants;

        /// <summary>
        /// Варианты районирования
        /// </summary>
        public ObservableCollection<PartitionVariants> PartitionVariants
        {
            get => _partitionVariants;
            set
            {
                _partitionVariants = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Событие, когда выбранный вариант районирования изменился.
        /// Предназаначено для подписки из UI
        /// </summary>
        public event Action SelectedPartitionVariantChanged;

        private PartitionVariants _selectedPartitionVariant;

        /// <summary>
        /// Выбранный вариант районирования
        /// </summary>
        public PartitionVariants SelectedPartitionVariant
        {
            get => _selectedPartitionVariant;
            set
            {
                _selectedPartitionVariant = value;
                SelectedPartitionVariantChanged?.Invoke();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Фильтр районов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FilterDistricts(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is Districts district &&
                         //Если район нулевой (аналог null), то выбирать его всегда
                         (district.PartitionVariants == null ||
                          //Если вариант районирования выбран (хоть какой)
                          SelectedPartitionVariant != null &&
                         //Если район принадлежит выбранному варианту районирования, то выбрать его
                         district.PartitionVariants.Id == SelectedPartitionVariant.Id);
        }

        private ObservableCollection<Districts> _districts;

        /// <summary>
        /// Районы
        /// </summary>
        public ObservableCollection<Districts> Districts
        {
            get => _districts;
            set
            {
                _districts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Событие необходимости отфильтровать коллекцию квадратов.
        /// Предназаначено для подписки из UI
        /// </summary>
        public event Action SquareHaveToBeFiltered;

        private Districts _selectedDistrict;

        /// <summary>
        /// Выбранный район
        /// </summary>
        public Districts SelectedDistrict
        {
            get => _selectedDistrict;
            set
            {
                _selectedDistrict = value;
                SquareHaveToBeFiltered?.Invoke();
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Фильтр коллекции квадратов
        /// </summary>
        /// <remarks>Логика выбора квадрата:
        /// Если не выбран вариант районирования, то показать все квадраты, не показывать районы;
        /// Если выбран вариант районирования, то показать районы этого варианта, показать все квадраты этого варианта;
        /// Если выбран вариант районирования и район, то показать квадраты этого района. 
        /// </remarks>
        public void FilterSquares(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is Squares square &&
                         //Если ничего не выбрано либо выбраны нулевые варианты, показать все квадраты
                         ((SelectedPartitionVariant == null || SelectedPartitionVariant.Id == 0) &&
                          (SelectedDistrict == null || SelectedDistrict.Id == 0) ||
                          //Если выбран вариант районирования и не выбран район (либо выбран нулевой)
                          SelectedPartitionVariant != null &&
                          (SelectedDistrict == null || SelectedDistrict.Id == 0) &&
                          square.Districts.PartitionVariants.Id == SelectedPartitionVariant.Id ||
                          //Если выбран и вариант районирования, и район
                          SelectedPartitionVariant != null && SelectedDistrict != null &&
                          square.Districts.Id == SelectedDistrict.Id) &&
                          //Если свойство фильтра названия квадрата пустое или входит в название квадрата
                          (string.IsNullOrWhiteSpace(FilterSquareName) || square.Name.Contains(FilterSquareName));
        }

        private string _filterSquareName;

        /// <summary>
        /// Строка, по которой также фильтруется коллекция квадратов
        /// </summary>
        public string FilterSquareName
        {
            get => _filterSquareName;
            set
            {
                _filterSquareName = value;
                SquareHaveToBeFiltered?.Invoke();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Squares> _squares;

        /// <summary>
        /// Квадраты
        /// </summary>
        public ObservableCollection<Squares> Squares
        {
            get => _squares;
            set
            {
                _squares = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private ObservableCollection<Grounds> _grounds;

        /// <summary>
        /// Грунты
        /// </summary>
        public ObservableCollection<Grounds> Grounds
        {
            get => _grounds;
            set
            {
                _grounds = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Gears> _gears;

        /// <summary>
        /// Орудия лова
        /// </summary>
        public ObservableCollection<Gears> Gears
        {
            get => _gears;
            set
            {
                _gears = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<WindDirects> _windDirections;

        /// <summary>
        /// Направления ветра
        /// </summary>
        public ObservableCollection<WindDirects> WindDirections
        {
            get => _windDirections;
            set
            {
                _windDirections = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Создает новую станцию
        /// </summary>
        public override void CreateNewItem()
        {
            NewItem = new Stations()
            {
                Voyage = ParentItem,
                Date = ParentItem.DateStart
            };
        }
    }
}
