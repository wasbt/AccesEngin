using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class REF_InfoGeneraleMetadata
    {
        [Display(Name = "#")]
        public long Id { get; set; }

        [Display(Name = "Rubrique d'info generale")]
        public long InfoGeneralRubriqueId { get; set; }

        [Display(Name = "Titre")]
        public string Name { get; set; }

        [Display(Name = "Date de créarion")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Ajputer par")]
        public string CreatedBy { get; set; }
    }
}
