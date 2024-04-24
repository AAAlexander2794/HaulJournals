using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsDAL.Concrete;
using HaulJournalsDAL.Entities;
using HaulJournalsEditing.Main;
using HaulJournalsEditing.Main.Journals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClassLibrary.BaseViewModel.Enums;

namespace HaulJournalsUnitTests
{
    [TestClass]
    public class TestViewModels
    {
        [TestMethod]
        public void TestOnPropertyChangedVoyages()
        {
            VoyagesViewModel voyagesViewModel = new VoyagesViewModel();
            bool flag = false;
            voyagesViewModel.SelectedItemChanged += vm =>
            {
                flag = true;
            };
            voyagesViewModel.Collection = new ObservableCollection<Voyages>()
            {
                new Voyages(),
                new Voyages(),
                new Voyages()
            };
            //voyagesViewModel.SelectedItem = voyagesViewModel.Collection[1];

            Assert.AreEqual(true, flag);


        }
    }
}
