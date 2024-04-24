using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaulJournalsImport.InitialClasses
{
    /// <summary>
    /// Строка таблицы с префиксом "v"
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ClassV
    {
        public string CFORM { get; set; }

        public string CORL { get; set; }

        public string NDATAR { get; set; }

        public string KDATAR { get; set; }

        public string NR { get; set; }

        public string NSUD { get; set; }

        public string NG { get; set; }

        public string NST { get; set; }

        public string KVL { get; set; }

        public string DATA { get; set; }

        public string EDVRD { get; set; }

        public string EDVRV { get; set; }

        public string KODVR { get; set; }

        public string DLINAVR1 { get; set; }

        public string DLINAVR2 { get; set; }

        public string SHTVR { get; set; }

        public string VESVR { get; set; }
    }
}
