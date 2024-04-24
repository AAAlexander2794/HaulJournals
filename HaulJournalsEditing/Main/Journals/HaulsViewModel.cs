using System.Collections.ObjectModel;
using System.Windows.Data;
using HaulJournalsDAL.Entities;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Journals
{
    public sealed class HaulsViewModel : BaseViewModel<Hauls, Stations>
    {
        private ObservableCollection<Fishes> _fishes;

        /// <summary>
        /// Типы рыбы
        /// </summary>
        public ObservableCollection<Fishes> Fishes
        {
            get => _fishes;
            set
            {
                _fishes = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Фильтр коллекции уловов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is Hauls haul
                ;
            //&& Collection.Contains(haul) 
            //&& ParentItem?.Hauls != null
            //&& ParentItem.Hauls.Contains(haul);
        }

        public override void CreateNewItem()
        {
            NewItem = new Hauls()
            {
                Stations = ParentItem
            };
        }

    }
}
