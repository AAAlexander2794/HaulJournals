using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using HaulJournalsDAL.Concrete;
using HaulJournalsDAL.Entities;
using HaulJournalsEditing.Main.Commands;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Journals
{

    public sealed class VoyagesViewModel : BaseViewModel<Voyages, UserInfo>
    {
        /// <summary>
        /// Дополнительная команда финализации рейса
        /// </summary>
        public FinalizeVoyageCommand FinalizeVoyage { get; }

        private ObservableCollection<VoyageTypes> _voyageTypes;
        /// <summary>
        /// Коллекция типов рейса
        /// </summary>
        public ObservableCollection<VoyageTypes> VoyageTypes { get=>_voyageTypes;
            set
            {
                _voyageTypes = value;
                OnPropertyChanged();
            } }

        /// <summary>
        /// Фильтр коллекции рейсов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is Voyages item
                         && Collection.Contains(item)
                         && ParentItem != null
                         && item.UserId == ParentItem.Id;
        }

        #region Судно

        private ObservableCollection<Vessels> _vessels;

        /// <summary>
        /// Коллекция судов
        /// </summary>
        public ObservableCollection<Vessels> Vessels
        {
            get => _vessels;
            set
            {
                _vessels = value;
                OnPropertyChanged();
            } 
        }

        private ObservableCollection<string> _vesselTypes;

        /// <summary>
        /// Коллекция типов судна без повторений (distinct)
        /// </summary>
        public ObservableCollection<string> VesselTypes
        {
            get =>_vesselTypes;
            set
            {
                _vesselTypes = value;
                OnPropertyChanged();
            } 
        }

        /// <summary>
        /// Событие (для подписки обновления ресурсов представления)
        /// </summary>
        public event Action SelectedVesselTypeChanged;

        private string _selectedVesselType;

        /// <summary>
        /// Выбранный тип судна (для фильтрации коллекции судов)
        /// </summary>
        public string SelectedVesselType
        {
            get => _selectedVesselType;
            set
            {
                _selectedVesselType = value;
                SelectedVesselTypeChanged?.Invoke();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Метод фильтрации представления коллекции (для CollectionViewSource)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FilterVessels(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is Vessels item &&
                (item.Type == SelectedVesselType || string.IsNullOrWhiteSpace(SelectedVesselType));
        }

        #endregion
     
        /// <summary>
        /// Конструктор.  
        /// Initializes a new instance of the <see cref="VoyagesViewModel"/> class.
        /// </summary>
        public VoyagesViewModel() : base()
        {
            FinalizeVoyage = new FinalizeVoyageCommand(this);
        }

        
        /// <inheritdoc />
        /// <summary>
        /// Создаем новый рейс
        /// </summary>
        public override void CreateNewItem()
        {
            //На данный момент предусмотрен только один тип рейса, его и выбираем
            var voyageTypeDefault = VoyageTypes.FirstOrDefault();
            int voyageTypeDefaultId = voyageTypeDefault?.Id ?? 0;
            NewItem = new Voyages()
            {
                VoyageType = voyageTypeDefault,
                VoyageTypeId = voyageTypeDefaultId,
                IsFinalized = false,
                UserId = ParentItem?.Id ?? 0
            };
        }

    }
}
