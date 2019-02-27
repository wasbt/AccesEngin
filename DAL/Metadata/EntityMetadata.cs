using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EntityMetadata
    {
        [Display(Name = "#")]
        public long Id { get; set; }

        [Display(Name = "Site")]
        public long SiteId { get; set; }

        [Display(Name = "Entité")]
        public string Name { get; set; }

        [Display(Name = "Date de création")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Crée par")]
        public string CreatedBy { get; set; }

    }
}
