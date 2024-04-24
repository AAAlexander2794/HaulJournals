using System.Windows.Data;
using HaulJournalsDAL.Entities;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Journals
{
    public class BioAnOldsViewModel : BaseViewModel<BioAnOld, BioAn>
    {
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = e.Item is BioAnOld bioAn;
            //&& Collection.Contains(bioAn)
            //&& ParentItem?.BioAnOld != null
            //&& ParentItem.BioAnOld.Contains(bioAn);
        }

        public override void CreateNewItem()
        {
            NewItem = new BioAnOld()
            {
                BioAn = ParentItem
            };
        }
    }
}
