using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.districts")]
    public partial class Districts : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Districts()
        {
            Squares = new HashSet<Squares>();
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

        private int _partitionVariantId;
        [Column("partition_variant_id")]
        public int PartitionVariantId { get=>_partitionVariantId;
            set
            {
                _partitionVariantId = value;
                OnPropertyChanged();
            }
        }

        private PartitionVariants _partitionVariant;
        public virtual PartitionVariants PartitionVariants { get=>_partitionVariant;
            set
            {
                _partitionVariant = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Squares> Squares { get; set; }

        public override void SetNavigationPropertiesToNull()
        {
            _partitionVariant = null;
        }
    }
}
