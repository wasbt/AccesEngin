using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class REF_NatureMatiereMetadata
    {
        [Display(Name = "#")]
        public long Id { get; set; }

        [Display(Name = "Nature")]
        public long TypeCheckListId { get; set; }

        [Display(Name = "Nature de la matiere")]
        public string Name { get; set; }

        [Display(Name = "Date de création")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Crée par")]
        public string CreatedBy { get; set; }

    }
}
