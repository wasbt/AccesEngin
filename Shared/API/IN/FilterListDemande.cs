using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.API.IN
{
    public class FilterListDemande
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long? TypeCheckListId { get; set; }
        public long? StatutId { get; set; }
        public bool OnlyControle { get; set; }
        public string Matricule { get; set; }
        public DateTime? DatePlanification { get; set; }
    }
}
