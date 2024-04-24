using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.gears")]
    public partial class Gears : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gears()
        {
            CatchFactors = new HashSet<CatchFactors>();
            Stations = new HashSet<Stations>();
        }

        private int _id;
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        private float? _openingWidth;
        [Column("opening_width")]
        public float? OpeningWidth { get=>_openingWidth;
            set
            {
                _openingWidth = value;
                OnPropertyChanged();
            }
        }

        private float? _openingHeight;
        [Column("opening_height")]
        public float? OpeningHeight { get=>_openingHeight;
            set
            {
                _openingHeight = value;
                OnPropertyChanged();
            } }

        private float? _openingSquare;
        [Column("opening_square")]
        public float? OpeningSquare { get=>_openingSquare;
            set
            {
                _openingSquare = value;
                OnPropertyChanged();
            }
        }

        private float? _meshSize;
        [Column("mesh_size")]
        public float? MeshSize { get=>_meshSize;
            set
            {
                _meshSize = value;
                OnPropertyChanged();
            }
        }

        private int? _gearTypeId;
        [Column("gear_type_id")]
        public int? GearTypeId { get=>_gearTypeId;
            set
            {
                _gearTypeId = value;
                OnPropertyChanged();
            }
        }

        private float? _length;
        public float? Length { get=>_length;
            set
            {
                _length = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatchFactors> CatchFactors { get; set; }

        private GearTypes _gearType;
        public virtual GearTypes GearTypes { get=>_gearType;
            set
            {
                _gearType = value;
                GearTypeId = value?.Id;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stations> Stations { get; set; }
    }
}
