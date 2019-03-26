using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppFile
    {
        public AppFile()
        {
            DemandeAccesEngin = new HashSet<DemandeAccesEngin>();
            DemandeResultatEntete = new HashSet<DemandeResultatEntete>();
        }
        public long Id { get; set; }
        public string SourceName { get; set; }
        public string SourceId { get; set; }
        public string ContainerName { get; set; }
        public string OriginalFileName { get; set; }
        public string SystemFileName { get; set; }
        public long FileSize { get; set; }
        public System.DateTime CreatedOn { get; set; }

        public virtual ICollection<DemandeAccesEngin> DemandeAccesEngin { get; set; }
        public virtual ICollection<DemandeResultatEntete> DemandeResultatEntete { get; set; }

    }
}
