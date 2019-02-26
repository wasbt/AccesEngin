using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.API.IN
{
    public class SaveNewResultatInfoGenerale
    {
        public ICollection<ResultatInfoGeneraleModel> ResultatInfoGeneral { get; set; }
        public long DemandeAccesEnginId { get; set; }

    }
    public class ResultatInfoGeneraleModel
    {

        public long GeneralInfoId { get; set; }

        public string ValueInfo { get; set; }


    }
}
