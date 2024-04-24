using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.grounds")]
    public partial class Grounds : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grounds()
        {
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stations> Stations { get; set; }
    }
}
