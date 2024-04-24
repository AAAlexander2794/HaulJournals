using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsDAL.Concrete;
using HaulJournalsDAL.Entities;

namespace HaulJournalsDAL.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Коллекции репозиторя биндятся к локальным данным модели данных (Context.Table.Local).
    /// Коллекции из репозитория один раз подключаются к Модели-представлению, то есть предполагается, что 
    /// сам объект коллекции изменяться не будет в течении всей жизни репозитория, не считая случая его 
    /// перезагрузки (<see cref="LoadData"/>), когда репозиторий остается, но контекст данных
    /// пересоздается заново, и коллекции переназначаются заново
    /// </remarks>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IRepositoryDatabase : INotifyPropertyChanged
    {
        /// <summary>
        /// Тип подключения контекста к БД
        /// </summary>
        /// <value>
        /// The type of the connection.
        /// </value>
        ConnectionTypes ConnectionType { get; }

        /// <summary>
        /// Возвращает подключенного пользователя
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        UserInfo User { get; }

        /// <summary>
        /// Коллекция пользователей
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        ObservableCollection<Users> Users { get; }

        #region Справочники

        /// <summary>
        /// Коллекция судов
        /// </summary>
        /// <value>
        /// The vessels.
        /// </value>
        ObservableCollection<Vessels> Vessels { get; }

        /// <summary>
        /// Коллекция типов рейсов
        /// </summary>
        /// <value>
        /// The voyage types.
        /// </value>
        ObservableCollection<VoyageTypes> VoyageTypes { get; }

        /// <summary>
        /// Коллекция типов орудий лова
        /// </summary>
        /// <value>
        /// The gear types.
        /// </value>
        ObservableCollection<GearTypes> GearTypes { get; }
            /// <summary>
        /// Коллекция орудий лова
        /// </summary>
        /// <value>
        /// The gears.
        /// </value>
        ObservableCollection<Gears> Gears { get; }

        /// <summary>
        /// Коллекция типов грунта
        /// </summary>
        /// <value>
        /// The grounds.
        /// </value>
        ObservableCollection<Grounds> Grounds { get; }

        /// <summary>
        /// Коллекция направлений ветра
        /// </summary>
        /// <value>
        /// The wind directs.
        /// </value>
        ObservableCollection<WindDirects> WindDirects { get; }

        /// <summary>
        /// Коллекция вариантов районирования
        /// </summary>
        /// <value>
        /// The partition variants.
        /// </value>
        ObservableCollection<PartitionVariants> PartitionVariants { get; }

        /// <summary>
        /// Коллекция районов
        /// </summary>
        /// <value>
        /// The districts.
        /// </value>
        ObservableCollection<Districts> Districts { get; }

        /// <summary>
        /// Коллекция квадратов
        /// </summary>
        /// <value>
        /// The squares.
        /// </value>
        ObservableCollection<Squares> Squares { get; }

        /// <summary>
        /// Коллекция типов видов рыбы (классификация видов рыбы)
        /// </summary>
        /// <value>
        /// The fish types.
        /// </value>
        ObservableCollection<FishTypes> FishTypes { get; }

        /// <summary>
        /// Коллекция видов рыбы
        /// </summary>
        /// <value>
        /// The fishes.
        /// </value>
        ObservableCollection<Fishes> Fishes { get; }

        /// <summary>
        /// Коллекция типов возраста
        /// </summary>
        /// <value>
        /// The ages.
        /// </value>
        ObservableCollection<Ages> Ages { get; }

        /// <summary>
        /// Коллекция стадий зрелости гонад
        /// </summary>
        /// <value>
        /// The gonads stages.
        /// </value>
        ObservableCollection<GonadsStages> GonadsStages { get; }

        /// <summary>
        /// Коллекция коэффициентов уловистости видов рыбы орудиями лова
        /// </summary>
        /// <value>
        /// The catch factors.
        /// </value>
        ObservableCollection<CatchFactors> CatchFactors { get; }

            #endregion

        #region Журналы

        /// <summary>
        /// Коллекция рейсов для пользователя
        /// </summary>
        /// <value>
        /// The voyages.
        /// </value>
        ObservableCollection<Voyages> Voyages { get; }

        /// <summary>
        /// Коллекция станций для пользователя
        /// </summary>
        /// <value>
        /// The stations.
        /// </value>
        ObservableCollection<Stations> Stations { get; }

        /// <summary>
        /// Коллекция уловов для пользователя
        /// </summary>
        /// <value>
        /// The hauls.
        /// </value>
        ObservableCollection<Hauls> Hauls { get; }

        /// <summary>
        /// Коллекция вариационных рядов для пользователя
        /// </summary>
        /// <value>
        /// The variable series.
        /// </value>
        ObservableCollection<VarSeries> VarSeries { get; }

        /// <summary>
        /// Коллекция биологических анализов для пользователя
        /// </summary>
        /// <value>
        /// The bio ans.
        /// </value>
        ObservableCollection<BioAn> BioAns { get; }

        /// <summary>
        /// Коллекция промеров для пользователя
        /// </summary>
        /// <value>
        /// The measurements.
        /// </value>
        ObservableCollection<Measurements> Measurements { get; }

        //void LoadMeasurements(long? foreignhKeyId = null);

        #endregion

        /// <summary>
        /// Подключает пользователя по имени и паролю
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        UserInfo Login(string username, string password);

        /// <summary>
        /// Загружает список пользователей
        /// </summary>
        /// <returns></returns>
        void LoadUsers();

        ///// <summary>
        ///// Загружает только справочники (перезагружая контекст)
        ///// </summary>
        //void LoadDirectories();

        /// <summary>
        /// Загружает всю информацию для подключенного пользователя
        /// (перезагружая контекст)
        /// </summary>
        void LoadData();

        /// <summary>
        /// Сохраняет все изменения в коллекциях
        /// </summary>
        void SaveChanges();
    }
}
