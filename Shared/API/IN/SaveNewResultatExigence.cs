using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.API.IN
{
    public class SaveNewResultatExigence
    {
        public ICollection<ControlResultatExigence> ResultatExigenceList { get; set; }
        public long DemandeAccesEnginId { get; set; }

    }
    public class ControlResultatExigence
    {


        public long CheckListExigenceId { get; set; }

        public bool IsConform { get; set; }

        public string Date { get; set; }

        public string Observation { get; set; }
    }
}
