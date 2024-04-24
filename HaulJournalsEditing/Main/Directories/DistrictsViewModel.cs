using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HaulJournalsDAL.Entities;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsEditing.Main.Directories
{
    public class DistrictsViewModel : BaseViewModel<Districts, PartitionVariants>
    {
        public override void FilterCollection(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
        }

        public override void CreateNewItem()
        {
            NewItem = new Districts()
            {
                PartitionVariants = ParentItem
            };
        }
    }
}
