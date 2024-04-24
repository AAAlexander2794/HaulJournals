using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.fishes")]
    public partial class Fishes : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fishes()
        {
            CatchFactors = new HashSet<CatchFactors>();
            Hauls = new HashSet<Hauls>();
        }

        private int _id;
        public int Id { get=>_id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        [Required]
        [StringLength(100)]
        public string Name { get=>_name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int? _weightMin;
        [Column("weight_min")]
        public int? WeightMin { get=>_weightMin;
            set
            {
                _weightMin = value;
                OnPropertyChanged();
            }
        }

        private int? _weightMax;
        [Column("weight_max")]
        public int? WeightMax { get=>_weightMax;
            set
            {
                _weightMax = value;
                OnPropertyChanged();
            }
        }

        private int? _lengthMin;
        [Column("length_min")]
        public int? LengthMin { get=>_lengthMin;
            set
            {
                _lengthMin = value;
                OnPropertyChanged();
            }
        }

        private int? _lengthMax;
        [Column("length_max")]
        public int? LengthMax { get=>_lengthMax;
            set
            {
                _lengthMax = value;
                OnPropertyChanged();
            }
        }

        private int? _fishTypeId;
        [Column("fish_type_id")]
        public int? FishTypeId { get=>_fishTypeId;
            set
            {
                _fishTypeId = value;
                OnPropertyChanged();
            }
        }

        private int? _key;
        public int? Key { get=>_key;
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatchFactors> CatchFactors { get; set; }

        private FishTypes _fishType;
        public virtual FishTypes FishTypes { get=>_fishType;
            set
            {
                _fishType = value;
                FishTypeId = value?.Id;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hauls> Hauls { get; set; }
    }
}
