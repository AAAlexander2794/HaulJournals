using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsDAL.Entities;

namespace HaulJournalsImport.InitialClasses
{
    /// <summary>
    /// Класс, содержащий в себе сущности, полученные из строки таблицы с префиксом "b"
    /// </summary>
    public class ClassBEntities
    {
        public Voyages Voyage { get; set; }

        public Stations Station { get; set; }

        public Hauls Haul { get; set; }

        public BioAn BioAn { get; set; }

        public BioAnOld BioAnOld { get; set; }
    }
}
