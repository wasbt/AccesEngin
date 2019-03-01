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
        public TypeCheckList TypeCheckList { get; set; }
        public TypeEngin TypeEngin { get; set; }
        public long exigencesNonApplicable { get; set; }
        public long exigencesApplicable { get; set; }
    }
}
