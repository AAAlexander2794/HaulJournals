using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.ages")]
    public partial class Ages : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ages()
        {
            BioAn = new HashSet<BioAn>();
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

        private string _number;
        [Required]
        [StringLength(3)]
        public string Number { get=>_number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        [Required]
        [StringLength(8000)]
        public string Name { get=>_name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BioAn> BioAn { get; set; }
    }
}
