using Shared.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.API.OUT
{
    public class DemandeDetail
    {
        public long Id { get; set; }

        public long TypeCheckListId { get; set; }

        public string TypeCheckListName { get; set; }

        public string TypeEnginName { get; set; }

        public string NatureMatiereName { get; set; }

        public string EntityName { get; set; }

        public string Observation { get; set; }

        public bool IsAutorise { get; set; }

        public DateTime DatePlannification { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedEmail { get; set; }

        public long? StatutId { get; set; }

        public string Statut { get; set; }

        public string StatutColor { get; set; }

        public string AutoriseName { get; set; }


        private bool _IsControlled;
        public bool IsControlled
        {
            get { return _IsControlled; }
            set
            {
                if (!StatutId.HasValue)
                {
                    _IsControlled = false;
                }
                else
                {
                    if (StatutId == (int)DemandeStatus.Refuser)
                    {
                        _IsControlled = false;
                    }
                    else
                    {
                        _IsControlled = true;
                    }
                }
            }
        }

        private bool _ButtonValideIsVisible;
        public bool ButtonValideIsVisible
        {
            get { return _ButtonValideIsVisible; }
            set
            {
                if (!StatutId.HasValue)
                {
                    if (StatutId == (int)DemandeStatus.Refuser)
                    {
                        _ButtonValideIsVisible = false;
                    }
                    else
                    {
                        _ButtonValideIsVisible = true;
                    }
                }
                else
                {
                    _ButtonValideIsVisible = false;
                }
            }
        }


    }
}
