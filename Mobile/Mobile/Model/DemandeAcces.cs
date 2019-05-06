using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Model
{
    public class DemandeAcces
    {

        public long Id { get; set; }

        public long TypeCheckListId { get; set; }

        public string TypeCheckListName { get; set; }

        public string TypeEnginName { get; set; }

        public string NatureMatiereName { get; set; }

        public string EntityName { get; set; }

        public string Observation { get; set; }

        public bool Autorise { get; set; }

        public DateTime DatePlannification { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedEmail { get; set; }

        public string Statut { get; set; }

        public string StatutColor { get; set; }

        public bool VisibleStatut
        {
            get
            {
                if (Statut == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }
    }
}
