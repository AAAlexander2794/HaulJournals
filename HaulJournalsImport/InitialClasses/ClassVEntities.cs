using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsDAL.Entities;

namespace HaulJournalsImport.InitialClasses
{
    /// <summary>
    /// Класс, содержащий в себе сущности, полученные из строки таблицы с префиксом "v"
    /// </summary>
    public class ClassVEntities
    {
        public Voyages Voyage { get; set; }

        public Stations Station { get; set; }

        public Hauls Haul { get; set; }

        public VarSeries VarSeries { get; set; }
    }
}
