using System.Collections.ObjectModel;
using System.Windows.Data;
using HaulJournalsDAL.Entities;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Journals
{
    public class BioAnsViewModel : BaseViewModel<BioAn, Hauls>
    {
        private ObservableCollection<GonadsStages> _gonadsStages;

        /// <summary>
        /// Коллекция стадий зрелости гонад
        /// </summary>
        public ObservableCollection<GonadsStages> GonadsStages
        {
            get => _gonadsStages;
            set
            {
                _gonadsStages = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Ages> _ages;

        /// <summary>
        /// Коллекция возрастов
        /// </summary>
        public ObservableCollection<Ages> Ages
        {
            get => _ages;
            set
            {
                _ages = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Фильтр коллекции биологических анализов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is BioAn bioAn;
            //&& Collection.Contains(bioAn)
            //&& ParentItem?.BioAn != null
            //&& ParentItem.BioAn.Contains(bioAn);
        }

        /// <summary>
        /// Создает новый биологический анализ
        /// </summary>
        public override void CreateNewItem()
        {
            NewItem = new BioAn()
            {
                Haul = ParentItem
            };
        }
    }
}
