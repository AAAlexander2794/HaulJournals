using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xaml;
using HaulJournalsDAL;
using HaulJournalsDAL.Abstract;
using HaulJournalsDAL.Concrete;
using HaulJournalsDAL.Entities;
using HaulJournalsDAL.Properties;
using HaulJournalsEditing.Enums;
using HaulJournalsEditing.Main.Directories;
using HaulJournalsEditing.Main.Journals;
using HaulJournalsEditing.Main.Login;
using HaulJournalsEditing.Properties;
using HaulJournalsUI;
using MyClassLibrary;

namespace HaulJournalsEditing.Main
{
    /// <summary>
    /// Данная модель-представление собирает остальные VM для выдачи на форму в качестве контекста,
    /// собранные здесь VM ничего не знают об этой MainVM, которая управляет ими.
    /// </summary>
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Команды MainViewModel
        /// </summary>
        public Commands.Commands Commands { get; }

        #region ViewModels

        #region Directories

        public VesselsViewModel VesselsViewModel { get; }

        public PartitionVariantsViewModel PartitionVariantsViewModel { get; }

        public DistrictsViewModel DistrictsViewModel { get; }

        public SquaresViewModel SquaresViewModel { get; }

        public VoyageTypesViewModel VoyageTypesViewModel { get; }

        public GearsViewModel GearsViewModel { get; }

        public GroundsViewModel GroundsViewModel { get; }

        public FishesViewModel FishesViewModel { get; }

        #endregion

        #region Journals

        /// <summary>
        /// Модель-представление рейсов
        /// </summary>
        public VoyagesViewModel VoyagesViewModel { get; }

        /// <summary>
        /// Модель-представление станций
        /// </summary>
        public StationsViewModel StationsViewModel { get; }

        /// <summary>
        /// Модель-представление уловов
        /// </summary>
        public HaulsViewModel HaulsViewModel { get; }

        /// <summary>
        /// Модель-представление вариационных рядов
        /// </summary>
        public VarSeriesViewModel VarSeriesViewModel { get; }

        /// <summary>
        /// Модель-представление биологического анализа
        /// </summary>
        public BioAnsViewModel BioAnsViewModel { get; }

        /// <summary>
        /// Модель-представление старых данных (из программы на FoxPro) биологического анализа
        /// </summary>
        /// <value>
        /// The bio an olds view model.
        /// </value>
        public BioAnOldsViewModel BioAnOldsViewModel { get; }

        /// <summary>
        /// Модель-представление промеров
        /// </summary>
        public MeasurementsViewModel MeasurementsViewModel { get; }

        #endregion

        #endregion

        #region Инкапсулированные главные коллекции

        //  Если в .xaml форме присваивать CollectionViewSource.Source коллекции напрямую из ViewModels,
        //то в режиме разработки будет выдаваться исключение ArgumentNullException, что мешает просматривать 
        //внешний вид формы. Чтобы этого избежать, используем дополнительные свойства, к которым и будем
        //привязывать форму

        #region Коллекции справочников

        private ObservableCollection<Vessels> _vesselsCollection;

        public ObservableCollection<Vessels> VesselsCollection
        {
            get => _vesselsCollection;
            private set
            {
                _vesselsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<VoyageTypes> _voyageTypesCollection;

        public ObservableCollection<VoyageTypes> VoyageTypesCollection
        {
            get => _voyageTypesCollection;
            set
            {
                _voyageTypesCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Gears> _gearsCollection;

        public ObservableCollection<Gears> GearsCollection
        {
            get => _gearsCollection;
            set
            {
                _gearsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Grounds> _groundsCollection;

        public ObservableCollection<Grounds> GroundsCollection
        {
            get => _groundsCollection;
            set
            {
                _groundsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PartitionVariants> _partitionVariantsCollection;

        public ObservableCollection<PartitionVariants> PartitionVariantsCollection
        {
            get => _partitionVariantsCollection;
            set
            {
                _partitionVariantsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Districts> _districtsCollection;

        public ObservableCollection<Districts> DistrictsCollection
        {
            get => _districtsCollection;
            set
            {
                _districtsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Squares> _squaresCollection;

        public ObservableCollection<Squares> SquaresCollection
        {
            get => _squaresCollection;
            set
            {
                _squaresCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Fishes> _fishesCollection;

        public ObservableCollection<Fishes> FishesCollection
        {
            get => _fishesCollection;
            set
            {
                _fishesCollection = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Коллекции журналов

        private ObservableCollection<Voyages> _voyagesCollection;

        /// <summary>
        /// Коллекция рейсов
        /// </summary>
        /// <value>
        /// The voyages collection.
        /// </value>
        public ObservableCollection<Voyages> VoyagesCollection
        {
            get => _voyagesCollection;
            private set
            {
                _voyagesCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Stations> _stationsCollection;

        /// <summary>
        /// Коллекция станций
        /// </summary>
        /// <value>
        /// The stations collection.
        /// </value>
        public ObservableCollection<Stations> StationsCollection
        {
            get => _stationsCollection;
            private set
            {
                _stationsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Hauls> _haulsCollection;

        /// <summary>
        /// Коллекция уловов
        /// </summary>
        /// <value>
        /// The hauls collection.
        /// </value>
        public ObservableCollection<Hauls> HaulsCollection
        {
            get => _haulsCollection;
            private set
            {
                _haulsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<VarSeries> _varSeriesCollection;

        /// <summary>
        /// Коллекция вариационных рядов
        /// </summary>
        /// <value>
        /// The variable series collection.
        /// </value>
        public ObservableCollection<VarSeries> VarSeriesCollection
        {
            get => _varSeriesCollection;
            private set
            {
                _varSeriesCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BioAn> _bioAnsCollection;

        /// <summary>
        /// Коллекция биологических анализов
        /// </summary>
        /// <value>
        /// The bio ans collection.
        /// </value>
        public ObservableCollection<BioAn> BioAnsCollection
        {
            get => _bioAnsCollection;
            private set
            {
                _bioAnsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BioAnOld> _bioAnOldsCollection;

        /// <summary>
        /// Коллекция старых биологических анализов (поля из программы на FoxPro)
        /// </summary>
        /// <value>
        /// The bio an olds collection.
        /// </value>
        public ObservableCollection<BioAnOld> BioAnOldsCollection
        {
            get => _bioAnOldsCollection;
            private set
            {
                _bioAnOldsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Measurements> _measurementsCollection;

        /// <summary>
        /// Коллекция промеров
        /// </summary>
        /// <value>
        /// The measurements collection.
        /// </value>
        public ObservableCollection<Measurements> MeasurementsCollection
        {
            get => _measurementsCollection;
            private set
            {
                _measurementsCollection = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Binds the incapsulated collections.
        /// </summary>
        /// <remarks>
        /// Инвокация события изменения коллекций вызывается, чтобы коллекции в xaml обновились
        /// </remarks>
        private void BindIncapsulatedCollections()
        {
            //Справочники
            VesselsCollection = VesselsViewModel.Collection;
            VesselsViewModel.CollectionChangedInvoke();
            VoyageTypesCollection = VoyageTypesViewModel.Collection;
            VoyageTypesViewModel.CollectionChangedInvoke();
            GearsCollection = GearsViewModel.Collection;
            GearsViewModel.CollectionChangedInvoke();
            GroundsCollection = GroundsViewModel.Collection;
            GroundsViewModel.CollectionChangedInvoke();
            PartitionVariantsCollection = PartitionVariantsViewModel.Collection;
            PartitionVariantsViewModel.CollectionChangedInvoke();
            DistrictsCollection = DistrictsViewModel.Collection;
            DistrictsViewModel.CollectionChangedInvoke();
            SquaresCollection = SquaresViewModel.Collection;
            SquaresViewModel.CollectionChangedInvoke();
            FishesCollection = FishesViewModel.Collection;
            FishesViewModel.CollectionChangedInvoke();
            //Журналы
            VoyagesCollection = VoyagesViewModel.Collection;
            VoyagesViewModel.CollectionChangedInvoke();
            StationsCollection = StationsViewModel.Collection;
            StationsViewModel.CollectionChangedInvoke();
            HaulsCollection = HaulsViewModel.Collection;
            HaulsViewModel.CollectionChangedInvoke();
            VarSeriesCollection = VarSeriesViewModel.Collection;
            VarSeriesViewModel.CollectionChangedInvoke();
            BioAnsCollection = BioAnsViewModel.Collection;
            BioAnsViewModel.CollectionChangedInvoke();
            BioAnOldsCollection = BioAnOldsViewModel.Collection;
            BioAnOldsViewModel.CollectionChangedInvoke();
            MeasurementsCollection = MeasurementsViewModel.Collection;
            MeasurementsViewModel.CollectionChangedInvoke();
        }

        #endregion

        #region Репозитории

        /// <summary>
        /// Репозиторий удаленной БД
        /// </summary>
        private IRepositoryDatabase RepositoryRemote { get; }

        /// <summary>
        /// Репозиторий локальной БД
        /// </summary>
        private IRepositoryDatabase RepositoryLocal { get; }

        private IRepositoryDatabase _selectedRepository;

        /// <summary>
        /// Выбранный репозиторий
        /// </summary>
        public IRepositoryDatabase SelectedRepository
        {
            get => _selectedRepository;
            private set
            {
                _selectedRepository = value;
                BindCollections();
                OnPropertyChanged();
                //Делаем возврат на вкладку рейсов
                Commands.BackToVoyages.Execute(null);
            }
        }

        private bool _localRepositorySelected;

        public bool LocalRepositorySelected
        {
            get => _localRepositorySelected;
            set
            {
                _localRepositorySelected = value;
                if (value) SelectedRepository = RepositoryLocal;
                OnPropertyChanged();
            }
        }

        private bool _remoteRepositorySelected;

        public bool RemoteRepositorySelected
        {
            get => _remoteRepositorySelected;
            set
            {
                _remoteRepositorySelected = value;
                if (value) SelectedRepository = RepositoryRemote;
                OnPropertyChanged();
            }
        }

        private bool _remoteRepositoryEnabled;

        /// <summary>
        /// Указывает, доступен ли удаленный репозиторий
        /// (удалось ли его создать)
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remote repository enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool RemoteRepositoryEnabled
        {
            get => _remoteRepositoryEnabled;
            set
            {
                _remoteRepositoryEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _localRepositoryEnabled;

        public bool LocalRepositoryEnabled
        {
            get => _localRepositoryEnabled;
            set
            {
                _localRepositoryEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Перезагружает контекст выбранного репозитория, чтобы сбросить изменения
        /// </summary>
        public void Reload()
        {
            //Пересоздаем контекст и загружаем данные для пользователя
            SelectedRepository.LoadData();
            BindCollections();
            Commands.BackToVoyages.Execute(null);
            MessageBox.Show($"Данные перезагружены");
        }

        /// <summary>
        /// Сохранить изменения контекста текущего репозитория в базу данных 
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                SelectedRepository.SaveChanges();
                MessageBox.Show($"Данные сохранены");
            }
            catch (Exception e)
            {
                MessageBox.Show(MyClassLibrary.ExceptionMessagesHandler.GetMessages(e));
            }
        }

        public MainViewModel()
        {
            //Вызываем метод, в котором действия для дебага
            Debug();
            //Команды
            Commands = new Commands.Commands(this);
            //Создаем репозитории
            try
            {
                RepositoryLocal = new RepositoryDatabase(ConnectionTypes.Local);
                LocalRepositoryEnabled = true;
                }
            catch
            {
                if (MessageBox.Show(Resources.MessageDeleteLocalRepository,
                        "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var model = DbConnectionManager.CreateConnection(ConnectionTypes.Local);
                    //Помечаем на удаление
                    model.Database.Delete();
                    //Сохраняем результат
                    model.SaveChanges();
                    //Модель больше не нужна - очищаем память
                    model.Dispose();
                    RepositoryLocal = new RepositoryDatabase(ConnectionTypes.Local);
                    LocalRepositoryEnabled = true;
                }
                else
                {
                    ////throw;//todo debug
                    RepositoryLocal = null;
                    LocalRepositoryEnabled = false;
                }
            }
            //Удаленный репозиторий может не создасться
            try
            {
                RepositoryRemote = new RepositoryDatabase(ConnectionTypes.Remote);
                RemoteRepositoryEnabled = true;
            }
            catch
            {
                RepositoryRemote = null;
                RemoteRepositoryEnabled = false;
            }
            //Из удаленной базы данных переписываем справочники в локальный репозиторий
            if (RemoteRepositoryEnabled && LocalRepositoryEnabled) RefreshRepositoryLocalUsers();
            //Создаем ViewModels
            //Directories
            VesselsViewModel = new VesselsViewModel();
            VoyageTypesViewModel = new VoyageTypesViewModel();
            GearsViewModel = new GearsViewModel();
            GroundsViewModel = new GroundsViewModel();
            PartitionVariantsViewModel = new PartitionVariantsViewModel();
            DistrictsViewModel = new DistrictsViewModel();
            SquaresViewModel = new SquaresViewModel();
            FishesViewModel = new FishesViewModel();
            //Journals
            VoyagesViewModel = new VoyagesViewModel();
            StationsViewModel = new StationsViewModel();
            HaulsViewModel = new HaulsViewModel();
            VarSeriesViewModel = new VarSeriesViewModel();
            BioAnsViewModel = new BioAnsViewModel();
            BioAnOldsViewModel = new BioAnOldsViewModel();
            MeasurementsViewModel = new MeasurementsViewModel();
            //При событии авторизации запустить поиск пользователя в репозиториях
            LoginViewModel.OnLogin += Login;
            //Выбираем текущий репозиторий
            if (RemoteRepositoryEnabled) RemoteRepositorySelected = true;
            else if (LocalRepositoryEnabled) LocalRepositorySelected = true;
            else
            {
                throw new Exception("Ни один из видов хранения данных недоступен.\n" +
                                    "Проверьте подключение к интернету и наличие базы данных у вас на компьютере.");
            }
        }

        /// <summary>
        /// Запускает поиск пользователя в репозиториях
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void Login(string username, string password)
        {
            string loginMessage = "Доступные репозитории:\n";
            if (LocalRepositoryEnabled)
            {
                RepositoryLocal.Login(username, password);
                loginMessage += $" - Локальный репозиторий\n";
                //Загрузка данных
                RepositoryLocal.LoadData();
            }
            if (RemoteRepositoryEnabled)
            {
                RepositoryRemote.Login(username, password);
                loginMessage += $" - Удаленный репозиторий\n";
                //Загрузка данных
                RepositoryRemote.LoadData();
            }
            ////Обратная связь - подтверждение авторизации
            //AutoClosingMessageBox.Show(loginMessage, "", 1);
            //Привязываем коллекции
            BindCollections();
            //todo debug
            //ReferencesInfo refIOnfo = new ReferencesInfo()
            //{

            //};
            //refIOnfo.Gears = SelectedRepository.Gears.ToList();
            //refIOnfo.Vessels = SelectedRepository.Vessels.ToList();
            //refIOnfo.VoyageTypes = SelectedRepository.VoyageTypes.ToList();
            //refIOnfo.Users = SelectedRepository.Users.ToList();
            //XamlServices.Save("C://xaml.txt", SelectedRepository);
        }

        /// <summary>
        /// Привязывает коллекции ViewModels к коллекциям выбранного репозитория,
        /// также запускаем <see cref="BindIncapsulatedCollections"/>
        /// </summary>
        private void BindCollections()
        {
            if (SelectedRepository == null) return;
            //СПРАВОЧНИКИ
            //todo для справочников с внешними ключами сделать родительские коллекции 
            VesselsViewModel.Collection = SelectedRepository.Vessels;
            VoyageTypesViewModel.Collection = SelectedRepository.VoyageTypes;
            GearsViewModel.Collection = SelectedRepository.Gears;
            GroundsViewModel.Collection = SelectedRepository.Grounds;
            PartitionVariantsViewModel.Collection = SelectedRepository.PartitionVariants;
            DistrictsViewModel.Collection = SelectedRepository.Districts;
            SquaresViewModel.Collection = SelectedRepository.Squares;
            FishesViewModel.Collection = SelectedRepository.Fishes;
            //ЖУРНАЛЫ
            //Рейсам нужен актуальный пользователь
            VoyagesViewModel.ParentItem = SelectedRepository.User;
            //Рейсы
            VoyagesViewModel.Collection = SelectedRepository.Voyages;
            VoyagesViewModel.Vessels = SelectedRepository.Vessels;
            VoyagesViewModel.VoyageTypes = SelectedRepository.VoyageTypes;
            //Типы судна
            VoyagesViewModel.VesselTypes =
                new ObservableCollection<string>((from vessel in SelectedRepository.Vessels select vessel.Type)
                    .Distinct().ToList());
            //Станции
            StationsViewModel.Collection = SelectedRepository.Stations;
            StationsViewModel.PartitionVariants = SelectedRepository.PartitionVariants;
            StationsViewModel.Districts = SelectedRepository.Districts;
            StationsViewModel.Squares = SelectedRepository.Squares;
            StationsViewModel.Gears = SelectedRepository.Gears;
            StationsViewModel.Grounds = SelectedRepository.Grounds;
            StationsViewModel.WindDirections = SelectedRepository.WindDirects;
            //Уловы
            HaulsViewModel.Collection = SelectedRepository.Hauls;
            HaulsViewModel.Fishes = SelectedRepository.Fishes;
            //Вариационный ряд
            VarSeriesViewModel.Collection = SelectedRepository.VarSeries;
            //Биологический анализ
            BioAnsViewModel.Collection = SelectedRepository.BioAns;
            BioAnsViewModel.Ages = SelectedRepository.Ages;
            BioAnsViewModel.GonadsStages = SelectedRepository.GonadsStages;
            //Промеры
            MeasurementsViewModel.Collection = SelectedRepository.Measurements;
            //
            VesselsViewModel.Collection = SelectedRepository.Vessels;
            //Биндим коллекции, которые созданы в this, чтобы к ним привязывать dataGrids формы
            BindIncapsulatedCollections();
        }

        /// <summary>
        /// Обновляет справочники локального репозитория в соответствии с 
        /// данными удаленного репозитория
        /// </summary>
        public void RefreshRepositoryLocalDirectories()
        {
            //Проверка на доступность
            if (!RemoteRepositoryEnabled) return;
            //Создаем временный экземпляр удаленного репозитория
            var tempRepos = new RepositoryDatabase(ConnectionTypes.Remote);
            //Справочники
            tempRepos.LoadDirectories();
            var vessels = tempRepos.Vessels;
            var voyageTypes = tempRepos.VoyageTypes;
            var gearTypes = tempRepos.GearTypes;
            var gears = tempRepos.Gears;
            var grounds = tempRepos.Grounds;
            var partitionVariants = tempRepos.PartitionVariants;
            var districts = tempRepos.Districts;
            var squares = tempRepos.Squares;
            var windDirects = tempRepos.WindDirects;
            var fishTypes = tempRepos.FishTypes;
            var fishes = tempRepos.Fishes;
            var ages = tempRepos.Ages;
            var gonadsStages = tempRepos.GonadsStages;
            var catchFactors = tempRepos.CatchFactors;
            if (vessels.Any())
            {
                foreach (var vessel in vessels)
                {
                    if (RepositoryLocal.Vessels.ToList().FirstOrDefault(u => u.Id == vessel.Id) != null) continue;
                    RepositoryLocal.Vessels.Add(vessel);
                }
            }
            if (voyageTypes.Any())
            {
                foreach (var voyageType in voyageTypes)
                {
                    if (RepositoryLocal.VoyageTypes.ToList().FirstOrDefault(u => u.Id == voyageType.Id) != null) continue;
                    RepositoryLocal.VoyageTypes.Add(voyageType);
                }
            }

            if (gearTypes.Any())
            {
                foreach (var gearType in gearTypes)
                {
                    if (RepositoryLocal.GearTypes.ToList().FirstOrDefault(g => g.Id == gearType.Id) != null) continue;
                    RepositoryLocal.GearTypes.Add(gearType);
                }
            }
            if (gears.Any())
            {
                foreach (var gear in gears)
                {
                    if (RepositoryLocal.Gears.ToList().FirstOrDefault(u => u.Id == gear.Id) != null) continue;
                    RepositoryLocal.Gears.Add(gear);
                }
            }
            if (grounds.Any())
            {
                foreach (var ground in grounds)
                {
                    if (RepositoryLocal.Grounds.ToList().FirstOrDefault(u => u.Id == ground.Id) != null) continue;
                    RepositoryLocal.Grounds.Add(ground);
                }
            }
            //Потом последовательно вносим данные, начиная с вариантов районирования
            if (partitionVariants.Any())
            {
                foreach (var partitionVariant in partitionVariants)
                {
                    if (RepositoryLocal.PartitionVariants.ToList().FirstOrDefault(u => u.Id == partitionVariant.Id) != null) continue;
                    RepositoryLocal.PartitionVariants.Add(partitionVariant);
                }
            }
            if (districts.Any())
            {
                foreach (var district in districts)
                {
                    if (RepositoryLocal.Districts.ToList().FirstOrDefault(u => u.Id == district.Id) != null) continue;
                    RepositoryLocal.Districts.Add(district);
                }
            }
            if (squares.Any())
            {
                foreach (var square in squares)
                {
                    if (RepositoryLocal.Squares.ToList().FirstOrDefault(u => u.Id == square.Id) != null) continue;
                    try
                    {
                        RepositoryLocal.Squares.Add(square);
                    }
                    catch { }
                }
            }
            if (windDirects.Any())
            {
                foreach (var windDirect in windDirects)
                {
                    if (RepositoryLocal.WindDirects.ToList().FirstOrDefault(u => u.Id == windDirect.Id) != null) continue;
                    RepositoryLocal.WindDirects.Add(windDirect);
                }
            }
            if (fishes.Any())
            {
                foreach (var fish in fishes)
                {
                    if (RepositoryLocal.Fishes.ToList().FirstOrDefault(u => u.Id == fish.Id) != null) continue;
                    RepositoryLocal.Fishes.Add(fish);
                }
            }
            if (fishTypes.Any())
            {
                foreach (var fishType in fishTypes)
                {
                    if (RepositoryLocal.FishTypes.ToList().FirstOrDefault(u => u.Id == fishType.Id) != null) continue;
                    RepositoryLocal.FishTypes.Add(fishType);
                }
            }
            if (ages.Any())
            {
                foreach (var age in ages)
                {
                    if (RepositoryLocal.Ages.Any(a => a.Id == age.Id)) continue;
                    RepositoryLocal.Ages.Add(age);
                }
            }
            if (gonadsStages.Any())
            {
                foreach (var gonadsStage in gonadsStages)
                {
                    if (RepositoryLocal.GonadsStages.ToList().FirstOrDefault(u => u.Id == gonadsStage.Id) != null) continue;
                    RepositoryLocal.GonadsStages.Add(gonadsStage);
                }
            }
            if (catchFactors.Any())
            {
                foreach (var catchFactor in catchFactors)
                {
                    if (RepositoryLocal.CatchFactors.ToList().FirstOrDefault(u => u.Id == catchFactor.Id) != null) continue;
                    RepositoryLocal.CatchFactors.Add(catchFactor);
                }
            }
            RepositoryLocal.SaveChanges();
        }

        /// <summary>
        /// Обновляет список пользователей в локальном репозитории
        /// в соответствии с удаленным репозиторием
        /// </summary>
        private void RefreshRepositoryLocalUsers()
        {
            var tempRepos = new RepositoryDatabase(ConnectionTypes.Remote);
            tempRepos.LoadUsers();
            var users = tempRepos.Users;
            RepositoryLocal.LoadUsers();
            foreach (var user in users)
            {
                if (!RepositoryLocal.Users.Where(u => u.Username == user.Username).ToList().Any())
                    RepositoryLocal.Users.Add(user);
            }
            RepositoryLocal.SaveChanges();
        }

        /// <summary>
        /// Загружает данные из локального репозитория в удаленный
        /// </summary>
        public void UploadLocalDataToRemote()
        {
            if (!LocalRepositoryEnabled || !RemoteRepositoryEnabled) throw new Exception("Один из репозиториев недоступен");
            var localRepos = new RepositoryDatabase(ConnectionTypes.Local);
            localRepos.LoadData(RepositoryLocal.User.Id);
            var wasErrors = false;
            foreach (var voyage in localRepos.Voyages)
            {
                using (var context = DbConnectionManager.CreateConnection(ConnectionTypes.Remote))
                {
                    try
                    {
                        voyage.SetNavigationPropertiesToNull();
                        foreach (var station in voyage.Stations)
                        {
                            station.SetNavigationPropertiesToNull();
                            foreach (var haul in station.Hauls)
                            {
                                haul.SetNavigationPropertiesToNull();
                                foreach (var bioAn in haul.BioAn)
                                {
                                    bioAn.SetNavigationPropertiesToNull();
                                    foreach (var bioAnOld in bioAn.BioAnOld)
                                    {
                                        bioAnOld.SetNavigationPropertiesToNull();
                                    }
                                }
                            }
                        }
                        context.Voyages.Add(voyage);
                        context.SaveChanges();
                    }
                    catch
                    {
                        wasErrors = true;
                        continue;
                    }
                }
            }
            string message = "Данные загружены в базу данных АзНИИРХ.\n" +
                             "Можете удалить данные сохраненные локально.";
            if (wasErrors) message += "\nДанные были загружены с ошибками.";
            RepositoryRemote.LoadData();
            MessageBox.Show(message);
        }

        #region TabItems

        private TabItems _selectedTabItem;

        /// <summary>
        /// Это хранит выбор из перечисления, при изменении выбора 
        /// меняет <see cref="SelectedTabItemIndex"/>
        /// </summary>
        public TabItems SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                _selectedTabItem = value;
                //Пересчет выбранного варианта из перечисления в его индекс
                SelectedTabItemIndex = (int)_selectedTabItem;
                OnPropertyChanged();
            }
        }

        private int _selectedTabItemIndex;

        /// <summary>
        /// К этому привязывается TabControl.SelectedIndex.
        /// Изменяется из <see cref="SelectedTabItem"/>
        /// </summary>
        public int SelectedTabItemIndex
        {
            get => _selectedTabItemIndex;
            set
            {
                _selectedTabItemIndex = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// Метод для дебага
        /// </summary>
        private void Debug()
        {
            
            //var repos = new RepositoryDatabase(ConnectionTypes.Remote);
            //repos.Login("user", "user");
            //repos.LoadData();
            //MessageBox.Show($"{repos.Voyages.Count}\n{repos.Stations.Count}\n{repos.Hauls.Count}");
            //var model1 = new HaulJournalsModel("HaulJournalsDbInner");
            //model1.CatchFactors.Load();
            //model1.FishTypes.Load();
            //model1.SaveChanges();
        }

    }
}
