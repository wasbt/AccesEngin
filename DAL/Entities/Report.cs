using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Report
    {
        public long Id { get; set; }
        public long DemandeAccesEnginId { get; set; }
        public string MotifReport { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual DemandeAccesEngin DemandeAccesEngin { get; set; }
    }
}
