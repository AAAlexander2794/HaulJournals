using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.squares")]
    public partial class Squares : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Squares()
        {
            StandartStations = new HashSet<StandartStations>();
            Stations = new HashSet<Stations>();
        }

        private int _id;
        public int Id { get=>_id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private int _districtId;
        [Column("district_id")]
        public int DistrictId { get=>_districtId;
            set
            {
                _districtId = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        [Required]
        [StringLength(10)]
        public string Name { get=>_name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        [Column("template_coords_n")]
        public decimal? TemplateCoordsN { get; set; }

        [Column("template_coords_e")]
        public decimal? TemplateCoordsE { get; set; }

        private Districts _districts;
        public virtual Districts Districts { get=>_districts;
            set
            {
                _districts = value;
                DistrictId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StandartStations> StandartStations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stations> Stations { get; set; }
    }
}
