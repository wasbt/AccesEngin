using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.API.IN
{
    public class SaveNewResultatExigence
    {
        public ICollection<ResultatExigence> ResultatExigenceList { get; set; }
        public long DemandeAccesEnginId { get; set; }

    }
    public class ResultatExigence
    {

        public long Id { get; set; }

        public long CheckListExigenceId { get; set; }

        public bool IsConform { get; set; }

        public DateTime Date { get; set; }

        public string Observation { get; set; }
    }
}
