using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.wind_directs")]
    public partial class WindDirects : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WindDirects()
        {
            Stations = new HashSet<Stations>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id { get; set; }

        [Column("name_full")]
        [StringLength(8000)]
        public string NameFull { get; set; }

        [Column("name_short_rus")]
        [Required]
        [StringLength(3)]
        public string NameShortRus { get; set; }

        [Column("name_short_eng")]
        [StringLength(3)]
        public string NameShortEng { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stations> Stations { get; set; }
    }
}
