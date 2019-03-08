using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("NatureMatiere")]

    public partial class NatureMatiere
    {

        public NatureMatiere()
        {
            DemandeAccesEngin = new HashSet<DemandeAccesEngin>();
        }

        public long Id { get; set; }

        public long TypeCheckListId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual TypeCheckList TypeCheckList { get; set; }

        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }
    }
}
