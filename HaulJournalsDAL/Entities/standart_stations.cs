using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("directories.standart_stations")]
    public partial class StandartStations : Entity
    {
        public int Id { get; set; }

        [Column("square_id")]
        public int? SquareId { get; set; }

        [StringLength(8000)]
        public string Name { get; set; }

        public virtual Squares Squares { get; set; }
    }
}
