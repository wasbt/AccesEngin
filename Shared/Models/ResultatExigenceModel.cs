using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ResultatExigenceModel
    {
        public DemandeAccesDto DemandeAccesDto { get; set; }
        public List<GroupInfoGeneral> ResultatValueGroupingInfoG { get; set; }
        public List<GroupExigence> ResultatValueGroupingExigence { get; set; }
    }

    public class ResultatValueExigence
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string conform { get; set; }
        public string Observation { get; set; }
        public string datetime { get; set; }
    }

    public class ResultatValueInfoGrenerale
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class GroupInfoGeneral
    {
        public string Key { get; set; }
        public List<ResultatValueInfoGrenerale> RsesultatInfoGrenerale { get; set; }
    }

    public class GroupExigence
    {
        public string Key { get; set; }
        public List<ResultatValueExigence> ResultatExigence { get; set; }
    }
}
