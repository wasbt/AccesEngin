using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class CheckListRubriqueDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ShowOrder { get; set; }
        public bool IsActif { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<CheckListExigenceDTO> Exigences { get; set; }
        public string ChecklistName { get; set; }
        public int ChecklistId { get; set; }
    }
}
