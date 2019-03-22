using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Model
{
    public class ResultatCheckList
    {
        public long DemandeAccesEnginId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<Resultats> ResultatsList { get; set; }

    }
    public class Resultats
    {
        public long CheckListExigenceId { get; set; }

        public bool IsConform { get; set; }

        public System.Nullable<DateTime> Date { get; set; }

        public string Observation { get; set; }
    }
}
