using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATAAL
{
    public class DemandeAccesEnginMetadata
    {
        [Display(Name = "#")]
        public long Id { get; set; }

        [Display(Name = "Type demande d'acces")]
        public long TypeCheckListId { get; set; }

        [Display(Name = "Oservation")]
        public string Observation { get; set; }

        [Display(Name = "Date de plannification")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DatePlannification { get; set; }

        [Display(Name = "Date de création")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Crée par")]
        public string CreatedBy { get; set; }
    }
}
