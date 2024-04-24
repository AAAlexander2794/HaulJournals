using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsEditing.Main;
using HaulJournalsUI;
using HaulJournalsUI.HaulJournalsEditingViews;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HaulJournalsUnitTests
{
    [TestClass]
    public class TestCollections
    {
        //[TestMethod]
        //public void Can_Login_Into_Repositories()
        //{
        //    MainViewModel mainViewModel = new MainViewModel();
        //    mainViewModel.Login("t", "t");

        //    Assert.AreSame(mainViewModel.VoyagesCollection, mainViewModel.VoyagesViewModel.Collection);
        //    Assert.AreSame(mainViewModel.StationsCollection, mainViewModel.StationsViewModel.Collection);
        //    Assert.AreSame(mainViewModel.HaulsCollection, mainViewModel.HaulsViewModel.Collection);
        //    Assert.AreSame(mainViewModel.VarSeriesCollection, mainViewModel.VarSeriesViewModel.Collection);
        //    Assert.AreSame(mainViewModel.BioAnsCollection, mainViewModel.BioAnsViewModel.Collection);
        //    Assert.AreSame(mainViewModel.MeasurementsCollection, mainViewModel.MeasurementsViewModel.Collection);
        //}

        [TestMethod]
        public void Proper_Collections_On_Reload()
        {
            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.Login("t", "t");

            mainViewModel.Reload();

            Assert.AreSame(mainViewModel.VoyagesCollection, mainViewModel.VoyagesViewModel.Collection);
            Assert.AreSame(mainViewModel.StationsCollection, mainViewModel.StationsViewModel.Collection);
            Assert.AreSame(mainViewModel.HaulsCollection, mainViewModel.HaulsViewModel.Collection);
            Assert.AreSame(mainViewModel.VarSeriesCollection, mainViewModel.VarSeriesViewModel.Collection);
            Assert.AreSame(mainViewModel.BioAnsCollection, mainViewModel.BioAnsViewModel.Collection);
            Assert.AreSame(mainViewModel.MeasurementsCollection, mainViewModel.MeasurementsViewModel.Collection);
        }
    }
}
