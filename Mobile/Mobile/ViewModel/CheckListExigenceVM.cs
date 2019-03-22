using Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModel
{
    public class CheckListExigenceVM 
    {
        private CheckListExigence checkListExigence;

        public CheckListExigenceVM(CheckListExigence exigence)
        {
            this.checkListExigence = exigence;
        }

        public string Name { get { return checkListExigence.Name; } }
        public string Observation
        {
            get
            {
                return checkListExigence.Observation;
            }
            set
            {
                checkListExigence.Observation = value;
            }
        }
        public long Id { get { return checkListExigence.Id; } }
        public bool IsConforme { get { return checkListExigence.IsConforme; } set { checkListExigence.IsConforme = value; } }
        public System.Nullable<DateTime> Date { get { return checkListExigence.Date; } set { checkListExigence.Date = value; } }


        public CheckListExigence CheckListExigence
        {
            get => checkListExigence;
        }
    }
}
