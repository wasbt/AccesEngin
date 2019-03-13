using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ResultatExigenceMetadata
    {
        public long Id { get; set; }

        public long DemandeResultatEnteteId { get; set; }

        public long CheckListExigenceId { get; set; }

        public bool IsConform { get; set; }

        public System.Nullable<DateTime> Date { get; set; }

        public string Observation { get; set; }

    }
}
