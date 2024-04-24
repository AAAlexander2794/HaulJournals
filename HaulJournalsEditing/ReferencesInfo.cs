using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaulJournalsDAL.Entities;

namespace HaulJournalsUI
{
    public class ReferencesInfo
    {
        public List<Users> Users { get; set; }

        public List<Vessels> Vessels{ get; set; }

        public List<VoyageTypes> VoyageTypes { get; set; }

        public List<Gears> Gears { get; set; }
    }
}
