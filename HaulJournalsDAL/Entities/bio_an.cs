using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("journals.bio_an")]
    public partial class BioAn : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BioAn()
        {
            BioAnOld = new HashSet<BioAnOld>();
        }

        private long _id;
        public long Id { get=>_id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private long _haulId;
        [Column("haul_id")]
        public long HaulId { get=>_haulId;
            set
            {
                _haulId = value;
                OnPropertyChanged();
            }
        }

        private double? _length;
        public double? Length { get=>_length;
            set
            {
                _length = value;
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

        private int? _sex;

        public int? Sex
        {
            get => _sex;
            set
            {
                _sex = value;
                OnPropertyChanged();
            }
        }

        private int? _ageId;
        [Column("age_id")]
        public int? AgeId { get=>_ageId;
            set
            {
                _ageId = value;
                OnPropertyChanged();
            }
        }

        private short? _gonadsStageId;
        [Column("gonads_stage_id")]
        public short? GonadsStageId { get=>_gonadsStageId;
            set
            {
                _gonadsStageId = value;
                OnPropertyChanged();
            }
        }

        private double? _belly;
        public double? Belly { get=>_belly;
            set
            {
                _belly = value;
                OnPropertyChanged();
            }
        }

        private double? _bowel;
        public double? Bowel { get=>_bowel;
            set
            {
                _bowel = value;
                OnPropertyChanged();
            }
        }

        private double? _richness;
        public double? Richness { get=>_richness;
            set
            {
                _richness = value;
                OnPropertyChanged();
            }
        }

        private double? _gonadsWeight;
        [Column("gonads_weight")]
        public double? GonadsWeight { get=>_gonadsWeight;
            set
            {
                _gonadsWeight = value;
                OnPropertyChanged();
            }
        }

        private int? _richnessBall;
        [Column("richness_ball")]
        public int? RichnessBall { get=>_richnessBall;
            set
            {
                _richnessBall = value;
                OnPropertyChanged();
            }
        }

        private Ages _age;
        public virtual Ages Age { get=>_age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        private GonadsStages _gonadsStage;
        public virtual GonadsStages GonadsStage { get=>_gonadsStage;
            set
            {
                _gonadsStage = value;
                OnPropertyChanged();
            }
        }

        private Hauls _haul;
        public virtual Hauls Haul { get=>_haul;
            set
            {
                _haul = value;
                HaulId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BioAnOld> BioAnOld { get; set; }

        public override void SetNavigationPropertiesToNull()
        {
            _gonadsStage = null;
            _age = null;
        }
    }
}
