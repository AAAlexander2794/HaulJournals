using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.meats")]
    public partial class Meats : Entity
    {
        public int Id { get; set; }

        [Column("id_char")]
        [Required]
        [StringLength(3)]
        public string IdChar { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
