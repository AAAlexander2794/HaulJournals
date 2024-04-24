using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.catch_factors")]
    public partial class CatchFactors : Entity
    {
        public int Id { get; set; }

        [Column("fish_id")]
        public int FishId { get; set; }

        [Column("gear_id")]
        public int GearId { get; set; }

        public float Value { get; set; }

        public virtual Fishes Fishes { get; set; }

        public virtual Gears Gears { get; set; }
    }
}
