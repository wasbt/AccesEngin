using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ValiderDemande
    {
        public long DemandeAccesEnginId { get; set; }
        public long StatutDemandeId { get; set; }
        public string Motif { get; set; }
        public DateTime DateSortie { get; set; }
    }
}
