using System.Windows.Data;
using HaulJournalsDAL.Entities;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Journals
{
    public class VarSeriesViewModel : BaseViewModel<VarSeries, Hauls>
    {

        /// <summary>
        /// Фильтр коллекции вариационных рядов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is VarSeries varSeries;
            //&& Collection.Contains(varSeries)
            //&& ParentItem?.VarSeries != null
            //&& ParentItem.VarSeries.Contains(varSeries);
        }

        /// <summary>
        /// Создает новый вариационный ряд
        /// </summary>
        public override void CreateNewItem()
        {
            NewItem = new VarSeries()
            {
                Hauls = ParentItem
            };
        }

    }
}
