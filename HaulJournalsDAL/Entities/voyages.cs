using System.Linq;
using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("journals.voyages")]
    public partial class Voyages : Entity, IEquatable<Voyages>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voyages()
        {
            Stations = new HashSet<Stations>();
        }

        private long _id;
        public long Id { get=>_id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private int _vesselId;
        [Column("vessel_id")]
        public int VesselId { get=>_vesselId;
            set
            {
                _vesselId = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateStart;
        [Required]
        [Column("date_start",TypeName = "date")]
        public DateTime? DateStart { get=>_dateStart;
            set
            {
                _dateStart = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateEnd;
        [Column("date_end",TypeName = "date")]
        public DateTime? DateEnd { get=>_dateEnd;
            set
            {
                _dateEnd = value;
                OnPropertyChanged();
            }
        }

        private int _userId;
        [Column("user_id")]
        public int UserId { get=>_userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        private bool _isFinalized;
        [Column("is_finalized")]
        public bool IsFinalized { get=>_isFinalized;
            set
            {
                _isFinalized = value;
                OnPropertyChanged();
            }
        }

        private int _voyageTypeId;
        [Column("voyage_type_id")]
        public int VoyageTypeId { get=>_voyageTypeId;
            set
            {
                _voyageTypeId = value;
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

        private bool? _isTest;

        [Column("is_test")]
        public bool? IsTest
        {
            get => _isTest;
            set
            {
                _isTest = value;
                OnPropertyChanged();
            }
        }

        private Vessels _vessel;
        public virtual Vessels Vessel { get=>_vessel;
            set
            {
                _vessel = value;
                VesselId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        private VoyageTypes _voyageType;
        public virtual VoyageTypes VoyageType { get=>_voyageType;
            set
            {
                _voyageType = value;
                VoyageTypeId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stations> Stations { get; set; }

        private Users _user;
        public virtual Users User { get=>_user;
            set
            {
                _user = value;
                UserId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        public override void SetNavigationPropertiesToNull()
        {
            _user = null;
            _voyageType = null;
            _vessel = null;
        }

        private string _gearName;
        [NotMapped]
        public string GearName
        {
            get
            {
                if (Stations.Count > 0)
                {
                    return Stations.ToList()[0].Gear?.Name;
                }

                return null;
            }
            set
            {
                _gearName = value;

                OnPropertyChanged();
            }
        }

        #region IEquatable

        public bool Equals(Voyages other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _vesselId == other._vesselId && _dateStart.Equals(other._dateStart) && _dateEnd.Equals(other._dateEnd);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Voyages) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _vesselId;
                hashCode = (hashCode * 397) ^ _dateStart.GetHashCode();
                hashCode = (hashCode * 397) ^ _dateEnd.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
