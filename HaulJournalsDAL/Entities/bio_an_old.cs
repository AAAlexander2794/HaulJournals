using MyClassLibrary.BaseViewModel.Abstract;

namespace HaulJournalsDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("journals.bio_an_old")]
    public partial class BioAnOld : Entity
    {
        public long Id { get; set; }

        [Column("bio_an_id")]
        public long BioAnId { get; set; }

        [StringLength(8000)]
        public string Dlina1 { get; set; }

        [StringLength(8000)]
        public string Gir { get; set; }

        [StringLength(8000)]
        public string Kishn { get; set; }

        [StringLength(8000)]
        public string Norm { get; set; }

        [StringLength(8000)]
        public string Normpr { get; set; }

        [Column("gel1gr")]
        [StringLength(8000)]
        public string Gel1Gr { get; set; }

        [Column("gel2gr")]
        [StringLength(8000)]
        public string Gel2Gr { get; set; }

        [Column("gel3gr")]
        [StringLength(8000)]
        public string Gel3Gr { get; set; }

        [StringLength(8000)]
        public string Kishgr { get; set; }

        [StringLength(8000)]
        public string Zrel1 { get; set; }

        [StringLength(8000)]
        public string Zrel2 { get; set; }

        [StringLength(8000)]
        public string Zrel3 { get; set; }

        [StringLength(8000)]
        public string Zrel4 { get; set; }

        [StringLength(8000)]
        public string Zrel5 { get; set; }

        private BioAn _bioAn;
        public virtual BioAn BioAn { get=>_bioAn;
            set
            {
                _bioAn = value;
                BioAnId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }
    }
}
