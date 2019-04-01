using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StatutDemande
    {
        public StatutDemande()
        {
            this.DemandeAccesEngin = new HashSet<DemandeAccesEngin>();

        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }

    }
}
