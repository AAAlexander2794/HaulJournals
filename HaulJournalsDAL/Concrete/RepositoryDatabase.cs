using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsDAL.Abstract;
using HaulJournalsDAL.Entities;
using HaulJournalsDAL.Properties;
using HaulJournalsModel = HaulJournalsDAL.Concrete.HaulJournalsModel;

namespace HaulJournalsDAL.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Метод <see cref="LoadData(int)"/> пересоздает коллекции, поэтому после его вызова необходимо 
    /// заново биндить все, что привязано к репозиторию
    /// </remarks>
    /// <seealso cref="HaulJournalsDAL.Abstract.IRepositoryDatabase" />
    public class RepositoryDatabase : IRepositoryDatabase
    {
        private HaulJournalsModel Context { get; set; }

        private UserInfo _user;

        /// <summary>
        /// Тип подключения контекста к БД
        /// </summary>
        /// <value>
        /// The type of the connection.
        /// </value>
        public ConnectionTypes ConnectionType { get; }

        /// <summary>
        /// Возвращает подключенного пользователя
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserInfo User
        {
            get => _user;
            private set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Коллекция пользователей
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollection<Users> Users { get; private set; }

        #region Справочники

        public ObservableCollection<Vessels> Vessels { get; private set; }
        public ObservableCollection<VoyageTypes> VoyageTypes { get; private set; }
        public ObservableCollection<GearTypes> GearTypes { get; private set; }
        public ObservableCollection<Gears> Gears { get; private set; }
        public ObservableCollection<Grounds> Grounds { get; private set; }
        public ObservableCollection<WindDirects> WindDirects { get; private set; }
        public ObservableCollection<PartitionVariants> PartitionVariants { get; private set; }
        public ObservableCollection<Districts> Districts { get; private set; }
        public ObservableCollection<Squares> Squares { get; private set; }
        public ObservableCollection<FishTypes> FishTypes { get; private set; }
        public ObservableCollection<Fishes> Fishes { get; private set; }
        public ObservableCollection<Ages> Ages { get; private set; }
        public ObservableCollection<GonadsStages> GonadsStages { get; private set; }
        public ObservableCollection<CatchFactors> CatchFactors { get; private set; }

        #endregion

        #region Журналы
        
        public ObservableCollection<Voyages> Voyages { get; private set; }
        public ObservableCollection<Stations> Stations { get; private set; }
        public ObservableCollection<Hauls> Hauls { get; private set; }
        public ObservableCollection<VarSeries> VarSeries { get; private set; }
        public ObservableCollection<BioAn> BioAns { get; private set; }
        public ObservableCollection<BioAnOld> BioAnsOld { get; private set; }
        public ObservableCollection<Measurements> Measurements { get; private set; }

        #endregion

        /// <summary>
        /// Конструктор.
        /// Initializes a new instance of the <see cref="RepositoryDatabase"/> class.
        /// </summary>
        /// <param name="connectionType">Type of the connection.</param>
        public RepositoryDatabase(ConnectionTypes connectionType)
        {
            ConnectionType = connectionType;
            Context = DbConnectionManager.CreateConnection(ConnectionType);
            //Если БД не соответствует модели - исключение (Серверная БД не проверяется)
            if (connectionType != ConnectionTypes.Remote && !Context.Database.CompatibleWithModel(true))
            {
                //Context.Dispose();
                throw new Exception(Resources.ExceptionDatabaseDoesNotMatchToModel);
            }
            //Привязка коллекция репозитория, чтобы они не были null
            BindDirectories();
            BindJournals();
        }

        /// <summary>
        /// Подключает пользователя по имени и паролю
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserInfo Login(string username, string password)
        {
            using (var context = DbConnectionManager.CreateConnection(ConnectionType))
            {
                var foundUser = (from user in context.Users
                        where user.Username == username
                              && user.Password == password
                        select user)
                    .FirstOrDefault();
                User = foundUser != null
                    ? new UserInfo(foundUser)
                    : throw new Exception("Пользователь с таким именем и паролем не найден.");
            }
            return User;
        }

        /// <summary>
        /// Загружает список пользователей
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void LoadUsers()
        {
            Context.Users.Local.Clear();
            Context.Users.Load();
            Users = Context.Users.Local;
        }

        /// <summary>
        /// Загружает только справочники (перезагружая контекст)
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void LoadDirectories()
        {
            ReloadContext();
            //Суда
            Context.Vessels.OrderBy(v => v.Name).Load();
            //Типы рейсов
            Context.VoyageTypes.Load();
            //Варианты районирования
            Context.PartitionVariants.Load();
            //Районы
            Context.Districts.Load();
            //Квадраты
            Context.Squares.Load();
            //Типы орудий лова
            Context.GearTypes.Load();
            //Орудия лова
            Context.Gears.Load();
            //Грунты
            Context.Grounds.Load();
            //Направления ветра
            Context.WindDirects.Load();
            //Типы видов рыб (дополнительная классификация видов рыбы) 
            Context.FishTypes.Load();
            //Виды рыбы
            Context.Fishes.Load();
            //Коэффициенты уловистости
            Context.CatchFactors.Load();
            //Возраста
            Context.Ages.Load();
            //Стадии зрелости гонад
            Context.GonadsStages.Load();
            //Привязываем коллекции репозитория к коллекциям контекста
            BindDirectories();
        }

        /// <summary>
        /// Загружает всю информацию для подключенного пользователя
        /// (перезагружая контекст)
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void LoadData()
        {
            //Если пользователь не указан перезагружаем контекст (читай стираем кэш данных)
            if (User == null)
            {
                ReloadContext();
                return;
            }
            //Пересоздаем контекст и загружаем справочники
            LoadDirectories();
            //Рейсы
            Context.Voyages
                .Where(v => v.UserId == User.Id)
                .Load();
            var voyagesIds = (from voyage in Context.Voyages.Local select voyage.Id).ToList();
            //Станции
            Context.Stations
                .Where(s => voyagesIds.Contains(s.VoyageId))
                .Load();
            var stationsIds = (from station in Context.Stations.Local select station.Id).ToList();
            //Уловы
            Context.Hauls
                .Where(h => stationsIds.Contains(h.StationId))
                .Load();
            var haulsIds = (from haul in Context.Hauls.Local select haul.Id).ToList();
            //Вариационные ряды
            Context.VarSeries
                .Where(v => haulsIds.Contains(v.HaulId))
                .Load();
            //Биологические анализы (включая таблицу старых данных BioAnOld)
            Context.BioAn
                .Where(b => haulsIds.Contains(b.HaulId))
                .Include(b => b.BioAnOld)
                .Load();
            //Промеры
            Context.Measurements
                .Where(m => haulsIds.Contains(m.HaulId))
                .Load();
            //Привязываем коллекции репозитория к коллекциям контекста
            BindJournals();
        }

        public void LoadData(int userId)
        {
            using (var context = DbConnectionManager.CreateConnection(ConnectionType))
            {
                var user = (from item in context.Users
                        where item.Id == userId
                        select item)
                    .FirstOrDefault();
                User = new UserInfo(user);
            }
            LoadData();
        }

        /// <summary>
        /// Сохраняет все изменения в коллекциях
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        #region private

        /// <summary>
        /// Пересоздает контекст с потерей несохраненных данных
        /// </summary>
        private void ReloadContext()
        {
            Context?.Dispose();
            Context = DbConnectionManager.CreateConnection(ConnectionType);
        }

        /// <summary>
        /// Привязывает коллекции справочников репозитория к соответствующим коллекциям
        /// в кэше контекста
        /// </summary>
        private void BindDirectories()
        {
            //Суда
            Vessels = Context.Vessels.Local;
            //Типы рейсов
            VoyageTypes = Context.VoyageTypes.Local;
            //Варианты районирования
            PartitionVariants = Context.PartitionVariants.Local;
            //Районы
            Districts = Context.Districts.Local;
            //Квадраты
            Squares = Context.Squares.Local;
            //Типы орудий лова
            GearTypes = Context.GearTypes.Local;
            //Орудия лова
            Gears = Context.Gears.Local;
            //Грунты
            Grounds = Context.Grounds.Local;
            //Направления ветра
            WindDirects = Context.WindDirects.Local;
            //Типы видов рыбы (дополнительная классификация)
            FishTypes = Context.FishTypes.Local;
            //Виды рыбы
            Fishes = Context.Fishes.Local;
            //Возраста
            Ages = Context.Ages.Local;
            //Стадии зрелости гонад
            GonadsStages = Context.GonadsStages.Local;
            //Коэффициенты уловистости
            CatchFactors = Context.CatchFactors.Local;
        }

        /// <summary>
        /// Привязывает коллекции журналов репозитория к соответствующим коллекциям
        /// в кэше контекста
        /// </summary>
        private void BindJournals()
        {
            //Рейсы
            Voyages = Context.Voyages.Local;
            //Станции
            Stations = Context.Stations.Local;
            //Уловы
            Hauls = Context.Hauls.Local;
            //Вариационные ряды
            VarSeries = Context.VarSeries.Local;
            //Биологические анализы
            BioAns = Context.BioAn.Local;
            //
            BioAnsOld = Context.BioAnOld.Local;
            //Промеры
            Measurements = Context.Measurements.Local;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [MyClassLibrary.Properties.NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
