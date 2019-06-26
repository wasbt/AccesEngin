using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class REF_CheckListExigenceMetadata
    {
        [Display(Name = "#")]
        public long Id { get; set; }

        [Display(Name = "Rubrique")]
        public long CheckListRubriqueId { get; set; }

        [Display(Name = "Exigence")]
        public string Name { get; set; }

        [Display(Name = "Order")]
        public int ShowOrder { get; set; }

        [Display(Name = "Activer")]
        public bool IsActif { get; set; }

        [Display(Name = "Date obligatoire")]
        public bool IsHasDate { get; set; }

        [Display(Name = "Date de création")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Crée par")]
        public string CreatedBy { get; set; }
    }
}
