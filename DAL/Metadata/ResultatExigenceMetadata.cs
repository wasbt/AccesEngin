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

        public long DemandeAccesEnginId { get; set; }

        public long CheckListExigenceId { get; set; }

        public bool IsConform { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.Nullable<DateTime> Date { get; set; }

        public string Observation { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
