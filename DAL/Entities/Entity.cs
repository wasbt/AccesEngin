using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Entity
    {
        public Entity()
        {
            DemandeAccesEngin = new HashSet<DemandeAccesEngin>();

        }
        public long Id { get; set; }

        public long SiteId { get; set; }

        public string Name { get; set; }

        public System.DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Site Site { get; set; }

        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }


    }
}
