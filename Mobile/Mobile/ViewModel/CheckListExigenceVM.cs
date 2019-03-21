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
        public long Id { get { return checkListExigence.Id; } }
        public bool IsConforme { get { return checkListExigence.IsConforme; } }


        public CheckListExigence CheckListExigence
        {
            get => checkListExigence;
        }
    }
}
