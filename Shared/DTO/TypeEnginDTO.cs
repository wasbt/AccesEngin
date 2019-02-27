using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class TypeEnginDTO
    {
        public long Id { get; set; }

        public int TypeCheckListId { get; set; }

        public string Name { get; set; }

        public string DureeEstimative { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
