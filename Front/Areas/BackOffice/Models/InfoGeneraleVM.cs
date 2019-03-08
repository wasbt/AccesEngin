using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front.Areas.BackOffice.Models
{
    public class InfoGeneraleVM
    {
        [Display(Name = "#")]
        public long Id { get; set; }

        [Display(Name = "Rubrique d'info generale")]
        public string InfoGeneralRubriqueName { get; set; }

        [Display(Name = "Titre")]
        public string Name { get; set; }

        [Display(Name = "Date de créarion")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Ajputer par")]
        public string CreatedBy { get; set; }

        [Display(Name = "Type demande d'acces")]
        public IEnumerable<string> TypeCheckListNames { get; set; }
    }
}



