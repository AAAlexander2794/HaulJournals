using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.vessels")]
    public partial class Vessels : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vessels()
        {
            Voyages = new HashSet<Voyages>();
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

        private string _type;
        [Required]
        [StringLength(4)]
        public string Type { get=>_type;
            set
            {
                _type = value;
                 OnPropertyChanged();
            }
        }

        private string _name;
        [Required]
        [StringLength(30)]
        public string Name { get=>_name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voyages> Voyages { get; set; }
    }
}
