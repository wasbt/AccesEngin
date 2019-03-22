using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Model
{
    public class CheckListExigence
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

        #region For Post

        public bool IsConforme { get; set; }
        public System.Nullable<DateTime> Date { get; set; }
        public string Observation { get; set; }
        

        #endregion

        public CheckListExigence(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}
