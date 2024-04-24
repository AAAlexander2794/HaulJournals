using System.Windows.Data;
using HaulJournalsDAL.Entities;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Journals
{
    public class MeasurementsViewModel : BaseViewModel<Measurements, Hauls>
    {
        /// <summary>
        /// Фильтр коллекции промеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is Measurements measurements;
            //&& Collection.Contains(measurements)
            //&& ParentItem?.Measurements != null
            //&& ParentItem.Measurements.Contains(measurements);
        }

        /// <summary>
        /// Создает новый промер
        /// </summary>
        public override void CreateNewItem()
        {
            NewItem = new Measurements()
            {
                Hauls = ParentItem
            };
        }
    }
}
