using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary.Annotations;

namespace HaulJournalsImport.InitialClasses
{
    /// <summary>
    /// Строка таблицы с перфиксом "p"
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ClassP
    {
        public string CFORM { get; set; }

        public string CORL { get; set; }

        public string NDATAR { get; set; }

        public string KDATAR { get; set; }

        public string NR { get; set; }

        public string NG { get; set; }

        public string NSUD { get; set; }

        public string NST { get; set; }

        public string KVL { get; set; }

        public string DATA { get; set; }

        public string EDPD { get; set; }

        public string EDPV { get; set; }

        public string KODP { get; set; }

        public string DLINAP1 { get; set; }

        public string DLINAP2 { get; set; }

        public string DLMAXP1 { get; set; }

        public string DLMAXP2 { get; set; }

        public string SHTP { get; set; }

        public string VESP { get; set; }
    }
}
