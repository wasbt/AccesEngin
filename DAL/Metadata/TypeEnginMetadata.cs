using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class REF_TypeEnginMetadata
    {
        [Display(Name = "#")]
        public long Id { get; set; }

        [Display(Name = "Nature")]
        public long TypeCheckListId { get; set; }

        [Display(Name = "Type d'engin")]
        public string Name { get; set; }

        [Display(Name = "Durée")]
        public string DureeEstimative { get; set; }

        [Display(Name = "Date de création")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Crée par")]
        public string CreatedBy { get; set; }
    }
}
