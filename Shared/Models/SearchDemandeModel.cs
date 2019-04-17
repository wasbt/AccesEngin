using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class SearchDemandeModel
    {
        public long? EntityId { get; set; }

        public string Matricule { get; set; }

        public long? StatutDemandeId { get; set; }

        public string Content { get; set; }

        public long? TypeCheckListId { get; set; }

        public bool? Autorise { get; set; }

        public bool? Controle { get; set; }

        public DateTime? DatePlannification { get; set; }
    }
}
