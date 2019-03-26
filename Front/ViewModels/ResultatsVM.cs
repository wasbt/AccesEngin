using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class ResultatsVM
    {
        public DemandeAccesEngin controle { get; set; }
        public DemandeResultatEntete DemandeResultat { get; set; }
        public TypeCheckList TypeCheckList { get; set; }
        public TypeEngin TypeEngin { get; set; }
        public NatureMatiere NatureMatiere { get; set; }
        public long exigencesNonApplicable { get; set; }
        public long exigencesApplicable { get; set; }


        public ICollection<ControlResultatExigence> ResultatExigenceList { get; set; }
        public long DemandeAccesEnginId { get; set; }
        public bool Autorise { get; set; }

        public class ControlResultatExigence
        {

            public long CheckListExigenceId { get; set; }

            public bool IsConform { get; set; }

            public string Date { get; set; }

            public string Observation { get; set; }
        }
    }
}
