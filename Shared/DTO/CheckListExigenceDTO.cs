using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class CheckListExigenceDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Poids { get; set; }
        public bool IsActif { get; set; }
        public int ShowOrder { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public long RubriqueId { get; set; }
        public string RubriqueName { get; set; }
        public bool IsHasDate { get; set; }
    }
}
