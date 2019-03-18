using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class DemandeAccesDto
    {

        public long Id { get; set; }

        public string TypeCheckListName { get; set; }

        public string TypeEnginName { get; set; }

        public string NatureMatiereName { get; set; }

        public string EntityName { get; set; }

        public string Observation { get; set; }

        public bool Autorise { get; set; }

        public DateTime DatePlannification { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
