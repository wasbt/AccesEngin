using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("DemandeResultatEntete")]
    public partial class DemandeResultatEntete
    {
        public DemandeResultatEntete()
        {
            ResultatExigence = new HashSet<ResultatExigence>();
        }
        public long Id { get; set; }

        public long DemandeAccesEnginId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual DemandeAccesEngin DemandeAccesEngin { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<ResultatExigence> ResultatExigence { get; set; }

    }
}
