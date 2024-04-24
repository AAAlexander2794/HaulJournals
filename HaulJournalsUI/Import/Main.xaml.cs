using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HaulJournalsDAL.Concrete;
using HaulJournalsDAL.Entities;
using HaulJournalsImport.Concrete;
using HaulJournalsImport.InitialClasses;

namespace HaulJournalsUI.Import
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        //private List<Voyages> VoyagesList { get; set; }

        private List<ClassA> ClassAList { get; set; }
        private List<ClassB> ClassBList { get; set; }
        private List<ClassP> ClassPList { get; set; }
        private List<ClassV> ClassVList { get; set; }

        private List<ClassAEntities> ClassAEntities { get; set; }
        private List<ClassBEntities> ClassBEntities { get; set; }
        private List<ClassPEntities> ClassPEntities { get; set; }
        private List<ClassVEntities> ClassVEntities { get; set; }

        private List<Voyages> Voyages { get; set; }
        //private List<Stations> Stations { get; set; }
        //private List<Hauls> Hauls { get; set; }
        //private List<VarSeries> VarSeries { get; set; }
        //private List<BioAn> BioAns { get; set; }
        //private List<Measurements> Measurements { get; set; }


        public Main()
        {
            InitializeComponent();
            ClassAEntities = new List<ClassAEntities>();
            ClassBEntities = new List<ClassBEntities>();
            ClassPEntities = new List<ClassPEntities>();
            ClassVEntities = new List<ClassVEntities>();

       
        }

        private void ReadXlsx(object sender, RoutedEventArgs e)
        {
            DataView viewA;
            DataView viewB;
            DataView viewP;
            DataView viewV;
            //try
            //{
                //Загружаем данные из таблиц в виде DataView
                viewA = DataImporter.ReadXlsx(tbA.Text).Tables[0].AsDataView();
                viewB = DataImporter.ReadXlsx(tbB.Text).Tables[0].AsDataView();
                viewP = DataImporter.ReadXlsx(tbP.Text).Tables[0].AsDataView();
                viewV = DataImporter.ReadXlsx(tbV.Text).Tables[0].AsDataView();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Не удалось прочитать указанные файлы. Возможно, они уже открыты.");
            //    return;
            //}
            //Парсим в классы с текстовыми полями
            ClassAList = Parser.ParseDataViewA(viewA);
            ClassBList = Parser.ParseDataViewB(viewB);
            ClassPList = Parser.ParseDataViewP(viewP);
            ClassVList = Parser.ParseDataViewV(viewV);
            //
            tblLog.Text += $"\nКоличество записей в оригинальных таблицах:\n" +
                           $"a: {ClassAList.Count}\n" +
                           $"b:{ClassBList.Count}\n" +
                           $"p:{ClassPList.Count}\n" +
                           $"v:{ClassVList.Count}";
            //Передаем на таблицы, чтобы посмотреть
            dgA.ItemsSource = ClassAList;
            dgB.ItemsSource = ClassBList;
            dgP.ItemsSource = ClassPList;
            dgV.ItemsSource = ClassVList;
            MessageBox.Show("Данные прочитаны");
        }

        private void ProcessFiles(object sender, RoutedEventArgs e)
        {
            
            //Загружаем списки справочников из нашей БД
            List<Squares> squaresList;
            List<WindDirects> windDirectsList;
            List<Grounds> groundsList;
            List<Gears> gearsList;
            List<Fishes> fishesList;
            List<Ages> agesList;
            List<GonadsStages> gonadsStagesList;
            using (var context = DbConnectionManager.CreateConnection(ConnectionTypes.Remote))
            {
                squaresList = context.Squares.Include(s => s.Districts).ToList();
                windDirectsList = context.WindDirects.ToList();
                groundsList = context.Grounds.ToList();
                gearsList = context.Gears.ToList();
                fishesList = context.Fishes.ToList();
                agesList = context.Ages.ToList();
                gonadsStagesList = context.GonadsStages.ToList();
            }
            //
            ClassAEntities = Parser.ParseClassAList(ClassAList, squaresList, windDirectsList, groundsList, gearsList,
                fishesList, out int kA);
            ClassBEntities = Parser.ParseClassBList(ClassBList, squaresList, gearsList, fishesList, agesList,
                gonadsStagesList, out int kB);
            ClassPEntities = Parser.ParseClassPList(ClassPList, squaresList, gearsList, fishesList, out int kP);
            ClassVEntities = Parser.ParseClassVList(ClassVList, squaresList, gearsList, fishesList, out int kV);
            //
            int k = 0;
            foreach (var classAEntitiese in ClassAEntities)
            {
                k++;
                //var fishid = 
                if (classAEntitiese.Haul.FishId == 0) throw new Exception("ID рыбы не казан");
            }

            k = 0;
            foreach (var classBEntitiese in ClassBEntities)
            {
                k++;
                if (classBEntitiese.Haul.FishId == 0) throw new Exception("ID рыбы не казан");
            }

            k = 0;
            foreach (var classPEntitiese in ClassPEntities)
            {
                k++;
                if (classPEntitiese.Haul.FishId == 0) throw new Exception("ID рыбы не казан");
            }

            k = 0;
            foreach (var classVEntitiese in ClassPEntities)
            {
                k++;
                if (classVEntitiese.Haul.FishId == 0) throw new Exception("ID рыбы не казан");
            }

            MessageBox.Show(
                $"{ClassAEntities.Count} {ClassBEntities.Count} {ClassPEntities.Count} {ClassVEntities.Count}");
            tblLog.Text += $"\nКоличество записей в оригинальных таблицах, переведенных в классы сущностей: \n" +
                           $"a: {ClassAEntities.Count} \n" +
                           $"b: {ClassBEntities.Count} \n" +
                           $"p: {ClassPEntities.Count} (ошибок: {kP})\n" +
                           $"v: {ClassVEntities.Count}";
        }

        private void Distinct(object sender, RoutedEventArgs e)
        {
            Voyages = Parser.UniteClassEntities(ClassAEntities, ClassBEntities, ClassPEntities, ClassVEntities);
            dgVoyages.ItemsSource = null;
            dgVoyages.ItemsSource = Voyages;
            dgBioAns.ItemsSource = Voyages.FirstOrDefault().Stations.FirstOrDefault().Hauls.FirstOrDefault().BioAn;
            //Считаем количество записей на всех уровнях
            var voyagesCount = 0;
            var stationsCount = 0;
            var haulsCount = 0;
            var bioAnsCount = 0;
            var measurementsCount = 0;
            var varSeriesCount = 0;
            foreach (var voyage in Voyages)
            {
                voyagesCount++;
                voyage.SetNavigationPropertiesToNull();
                foreach (var station in voyage.Stations)
                {
                    stationsCount++;
                    station.SetNavigationPropertiesToNull();
                    foreach (var haul in station.Hauls)
                    {
                        haulsCount++;
                        haul.SetNavigationPropertiesToNull();
                        foreach (var bioAn in haul.BioAn)
                        {
                            bioAnsCount++;
                            bioAn.SetNavigationPropertiesToNull();
                        }

                        foreach (var measurement in haul.Measurements)
                        {
                            measurementsCount++;
                        }

                        foreach (var varSeries in haul.VarSeries)
                        {
                            varSeriesCount++;
                        }
                    }
                }
            }

            tblLog.Text += $"\nКоличество записей после обработки:\n" +
                           $"Рейсы: {voyagesCount}\n" +
                           $"Станции: {stationsCount}\n" +
                           $"Уловы: {haulsCount}\n" +
                           $"Био анализы: {bioAnsCount}\n" +
                           $"Промеры: {measurementsCount}\n" +
                           $"Вар ряды: {varSeriesCount}";
            //MessageBox.Show($"{Voyages[0].Stations.FirstOrDefault().Hauls.Count}\n" +
            //                $"{Voyages[0].Stations.FirstOrDefault().Hauls.FirstOrDefault().BioAn.Count.ToString()}");
            MessageBox.Show("Данные обработаны");
        }

        private void ContinueVoyage(object sender, RoutedEventArgs e)
        {
            try
            {
                dgStations.ItemsSource = ((Voyages)dgVoyages.SelectedItem).Stations;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Проблема с просмотром станций рейса");
            }
            
        }

        private void ContinueStation(object sender, RoutedEventArgs e)
        {
            try
            {
                dgHauls.ItemsSource = ((Stations)dgStations.SelectedItem).Hauls;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Проблема с просмотром улова станции");
            }
        }

        private void ContinueHaul(object sender, RoutedEventArgs e)
        {
            try
            {
                dgVarSeries.ItemsSource = ((Hauls)dgHauls.SelectedItem).VarSeries;
                dgBioAns.ItemsSource = ((Hauls)dgHauls.SelectedItem).BioAn;
                dgMeasurements.ItemsSource = ((Hauls)dgHauls.SelectedItem).Measurements;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Проблема с просмотром анализова улова");
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                var repos = new RepositoryDatabase(ConnectionTypes.Remote);
                foreach (var voyage in Voyages)
                {
                    voyage.SetNavigationPropertiesToNull();
                    foreach (var station in voyage.Stations)
                    {
                        station.SetNavigationPropertiesToNull();
                        foreach (var haul in station.Hauls)
                        {
                            //if (haul.FishId == 0) throw new Exception("ID рыбы не указан");
                            haul.SetNavigationPropertiesToNull();
                            foreach (var bioAn in haul.BioAn)
                            {
                                bioAn.SetNavigationPropertiesToNull();
                            }
                        }
                    }
                    repos.Voyages.Add(voyage);
                }
                repos.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            //Считаем количество записей на всех уровнях
            var voyagesCount = 0;
            var stationsCount = 0;
            var haulsCount = 0;
            var bioAnsCount = 0;
            var measurementsCount = 0;
            var varSeriesCount = 0;
            foreach (var voyage in Voyages)
            {
                voyagesCount++;
                voyage.SetNavigationPropertiesToNull();
                foreach (var station in voyage.Stations)
                {
                    stationsCount++;
                    station.SetNavigationPropertiesToNull();
                    foreach (var haul in station.Hauls)
                    {
                        haulsCount++;
                        haul.SetNavigationPropertiesToNull();
                        foreach (var bioAn in haul.BioAn)
                        {
                            bioAnsCount++;
                            bioAn.SetNavigationPropertiesToNull();
                        }

                        foreach (var measurement in haul.Measurements)
                        {
                            measurementsCount++;
                        }

                        foreach (var varSeries in haul.VarSeries)
                        {
                            varSeriesCount++;
                        }
                    }
                }
            }

            var info = $"\nКоличество добавленных записей:\n" +
                           $"Рейсы: {voyagesCount}\n" +
                           $"Станции: {stationsCount}\n" +
                           $"Уловы: {haulsCount}\n" +
                           $"Био анализы: {bioAnsCount}\n" +
                           $"Промеры: {measurementsCount}\n" +
                           $"Вар ряды: {varSeriesCount}";

            MessageBox.Show("Данные сохранены в БД\n"+info);
        }


        private void UniteVoyages(object sender, RoutedEventArgs e)
        {
            List<Voyages> voyagesToUnite = new List<Voyages>();
            
            foreach (var item in dgVoyages.SelectedItems)
            {
                voyagesToUnite.Add((Voyages)item);
            }
            DateTime? minDateStart = (voyagesToUnite[0]).DateStart;
            DateTime? maxDateEnd = (voyagesToUnite[0]).DateEnd;
            
            foreach (var item in voyagesToUnite)
            {
                if (minDateStart > item.DateStart) minDateStart = item.DateStart;
                if (maxDateEnd == null || item.DateEnd.HasValue && maxDateEnd.Value < item.DateEnd.Value)
                    if (item.DateEnd != null)
                        maxDateEnd = item.DateEnd.Value;
            }

            //MessageBox.Show(voyagesToUnite[0].Stations.Count.ToString());

            voyagesToUnite[0].DateStart = minDateStart;
            voyagesToUnite[0].DateEnd = maxDateEnd;

            for (int i = 1; i < voyagesToUnite.Count; i++)
            {
                //MessageBox.Show(voyagesToUnite[i].Stations.Count.ToString());
                foreach (var station in voyagesToUnite[i].Stations)
                {
                    voyagesToUnite[0].Stations.Add(station);
                }
                Voyages.Remove(voyagesToUnite[i]);
            }

            //MessageBox.Show(voyagesToUnite[0].Stations.Count.ToString());

            dgVoyages.ItemsSource = null;
            dgVoyages.ItemsSource = Voyages;
        }

        private void SetDateEnd(object sender, RoutedEventArgs e)
        {
            var voyageToModify = (Voyages)dgVoyages.SelectedItem;

            voyageToModify.DateEnd = dateEnd.DisplayDate;

            dgVoyages.ItemsSource = null;
            dgVoyages.ItemsSource = Voyages;
        }
    }
}
