using HaulJournalsDAL.Concrete;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// ¬ <see cref="Square"/> есть автоматический расчет координат начала
    /// </remarks>
    /// <seealso cref="MyClassLibrary.BaseViewModel.Abstract.Entity" />
    [Table("journals.stations")]
    public partial class Stations : Entity, IEquatable<Stations>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stations()
        {
            Hauls = new HashSet<Hauls>();
        }

        private long _id;
        public long Id { get=>_id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private long _voyageId;
        [Column("voyage_id")]
        public long VoyageId { get=>_voyageId;
            set
            {
                _voyageId = value;
                OnPropertyChanged();
            }
        }

        private int? _squareId;
        [Column("square_id")]
        public int? SquareId { get=>_squareId;
            set
            {
                _squareId = value;
                OnPropertyChanged();
            }
        }

        private short? _windDirectId;
        [Column("wind_direct_id")]
        public short? WindDirectId { get=>_windDirectId;
            set
            {
                _windDirectId = value;
                OnPropertyChanged();
            }
        }

        private int? _windSpeed;
        [Column("wind_speed")]
        public int? WindSpeed { get=>_windSpeed;
            set
            {
                _windSpeed = value;
                OnPropertyChanged();
            }
        }

        private int? _waving;
        public int? Waving { get=>_waving;
            set
            {
                _waving = value;
                OnPropertyChanged();
            }
        }

        private float? _depth;
        public float? Depth { get=>_depth;
            set
            {
                _depth = value;
                OnPropertyChanged();
            }
        }

        private int? _groundId;
        [Column("ground_id")]
        public int? GroundId { get=>_groundId;
            set
            {
                _groundId = value;
                OnPropertyChanged();
            }
        }

        private decimal? _tempAir;
        [Column("temp_air")]
        public decimal? TempAir { get=>_tempAir;
            set
            {
                _tempAir = value;
                OnPropertyChanged();
            }
        }

        private decimal? _tempWaterSurface;
        [Column("temp_water_surface")]
        public decimal? TempWaterSurface { get=>_tempWaterSurface;
            set
            {
                _tempWaterSurface = value;
                OnPropertyChanged();
            }
        }

        private decimal? _tempWater5M;
        [Column("temp_water_5m")]
        public decimal? TempWater_5M { get=>_tempWater5M;
            set
            {
                _tempWater5M = value;
                OnPropertyChanged();
            }
        }

        private decimal? _tempWaterBottom;
        [Column("temp_water_bottom")]
        public decimal? TempWaterBottom { get=>_tempWaterBottom;
            set
            {
                _tempWaterBottom = value;
                OnPropertyChanged();
            }
        }

        private int? _s;
        public int? S { get=>_s;
            set
            {
                _s = value;
                OnPropertyChanged();
            }
        }

        private int? _o2;
        public int? O2 { get=>_o2;
            set
            {
                _o2 = value;
                OnPropertyChanged();
            }
        }

        private decimal _coordsStartN;
        [Column("coords_start_N")]
        public decimal CoordsStartN { get=>_coordsStartN;
            set
            {
                _coordsStartN = value;
                OnPropertyChanged();
            }
        }

        private decimal _coordsStartE;
        [Column("coords_start_E")]
        public decimal CoordsStartE { get=>_coordsStartE;
            set
            {
                _coordsStartE = value;
                OnPropertyChanged();
            }
        }

        private decimal? _coordsEndN;
        [Column("coords_end_N")]
        public decimal? CoordsEndN { get=>_coordsEndN;
            set
            {
                _coordsEndN = value;
                 OnPropertyChanged();
            }
        }

        private decimal? _coordsEndE;
        [Column("coords_end_E")]
        public decimal? CoordsEndE { get=>_coordsEndE;
            set
            {
                _coordsEndE = value;
                 OnPropertyChanged();
            }
        }

        private TimeSpan? _timeStart;
        [Column("time_start",TypeName = "time")]
        public TimeSpan? TimeStart { get=>_timeStart;
            set
            {
                _timeStart = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan? _timeEnd;
        [Column("time_end",TypeName = "time")]
        public TimeSpan? TimeEnd { get=>_timeEnd;
            set
            {
                _timeEnd = value;
                OnPropertyChanged();
            }
        }

        private int? _stationNumber;
        [Column("station_number")]
        public int? StationNumber { get=>_stationNumber;
            set
            {
                _stationNumber = value;
                 OnPropertyChanged();
            }
        }

        private int _gearId;
        [Column("gear_id")]
        public int GearId { get=>_gearId;
            set
            {
                _gearId = value;
                OnPropertyChanged();
            }
        }

        private float? _gearSquare;
        [Column("gear_square")]
        public float? GearSquare { get=>_gearSquare;
            set
            {
                _gearSquare = value;
                OnPropertyChanged();
            }
        }

        private float? _gearSpeed;
        [Column("gear_speed")]
        public float? GearSpeed { get=>_gearSpeed;
            set
            {
                _gearSpeed = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _date;
        [Column(TypeName = "date")]
        public DateTime? Date { get=>_date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private float? _transparency;
        public float? Transparency { get=>_transparency;
            set
            {
                _transparency = value;
                OnPropertyChanged();
            }
        }

        private string _note;
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        private Gears _gear;
        public virtual Gears Gear { get=>_gear;
            set
            {
                _gear = value;
                GearId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        private Grounds _ground;
        public virtual Grounds Ground { get=>_ground;
            set
            {
                _ground = value;
                GroundId = value?.Id;
                OnPropertyChanged();
            }
        }

        private Squares _square;
        public virtual Squares Square { get=>_square;
            set
            {
                _square = value;
                SquareId = value?.Id;
                //–асчитываем координаты по квадрату
                if (value != null)
                {
                    SquareDictionaries.AzovSea.Standart.GetCoords(value.Name, out decimal n, out decimal e);
                    CoordsStartN = n;
                    CoordsStartE = e;
                }
                OnPropertyChanged();
            }
        }

        private WindDirects _windDirect;
        public virtual WindDirects WindDirect { get=>_windDirect;
            set
            {
                _windDirect = value;
                WindDirectId = value?.Id;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hauls> Hauls { get; set; }

        private Voyages _voyage;
        public virtual Voyages Voyage { get=>_voyage;
            set
            {
                _voyage = value;
                VoyageId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        public override void SetNavigationPropertiesToNull()
        {
            _gear = null;
            _ground = null;
            _windDirect = null;
            _square = null;
        }

        #region IEquatable

        public bool Equals(Stations other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _coordsStartN == other._coordsStartN && _coordsStartE == other._coordsStartE && _gearId == other._gearId && _date.Equals(other._date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Stations) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _coordsStartN.GetHashCode();
                hashCode = (hashCode * 397) ^ _coordsStartE.GetHashCode();
                hashCode = (hashCode * 397) ^ _gearId;
                hashCode = (hashCode * 397) ^ _date.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
