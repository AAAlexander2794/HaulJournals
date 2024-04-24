using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("journals.measurements")]
    public partial class Measurements : Entity
    {
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

        private double? _length1;
        public double? Length1 { get=>_length1;
            set
            {
                _length1 = value;
                OnPropertyChanged();
            }
        }

        private double? _length2;
        public double? Length2 { get=>_length2;
            set
            {
                _length2 = value;
                OnPropertyChanged();
            }
        }

        private double? _lengthMin;
        [Column("length_min")]
        public double? LengthMin { get=>_lengthMin;
            set
            {
                _lengthMin = value;
                OnPropertyChanged();
            }
        }

        private double? _lengthMax;
        [Column("length_max")]
        public double? LengthMax { get=>_lengthMax;
            set
            {
                _lengthMax = value;
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

        private double? _weight;
        public double? Weight { get=>_weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        private Hauls _haul;
        public virtual Hauls Hauls { get=>_haul;
            set
            {
                _haul = value;
                HaulId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

    }
}
