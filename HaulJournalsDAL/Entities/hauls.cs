using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("journals.hauls")]
    public partial class Hauls : Entity, IEquatable<Hauls>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hauls()
        {
            BioAn = new HashSet<BioAn>();
            Measurements = new HashSet<Measurements>();
            VarSeries = new HashSet<VarSeries>();
        }

        private long _id;
        public long Id { get=>_id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private long _stationId;
        [Column("station_id")]
        public long StationId { get=>_stationId;
            set
            {
                _stationId = value;
                OnPropertyChanged();
            }
        }

        private int _fishId;
        [Column("fish_id")]
        public int FishId { get=>_fishId;
            set
            {
                _fishId = value;
                OnPropertyChanged();
            }
        }

        private double? _weight;
        public double? Weight { get=>_weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        private int? _pieces;
        public int? Pieces { get=>_pieces;
            set
            {
                _pieces = value;
                OnPropertyChanged();
            }
        }

        private string _note;
        [StringLength(8000)]
        public string Note { get=>_note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        private float? _numOfSamples;
        [Column("num_of_samples")]
        public float? NumOfSamples { get=>_numOfSamples;
            set
            {
                _numOfSamples = value;
                OnPropertyChanged();
            }
        }

        private Fishes _fish;
        public virtual Fishes Fish { get=>_fish;
            set
            {
                _fish = value;
                FishId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BioAn> BioAn { get; set; }

        private Stations _station;
        public virtual Stations Stations { get=>_station;
            set
            {
                _station = value;
                StationId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Measurements> Measurements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VarSeries> VarSeries { get; set; }

        public override void SetNavigationPropertiesToNull()
        {
            _fish = null;
        }

        #region IEquatable

        public bool Equals(Hauls other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _fishId == other._fishId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Hauls)obj);
        }

        public override int GetHashCode()
        {
            return _fishId;
        }

        #endregion


    }
}
