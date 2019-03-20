using Mobile.Model;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModel
{
    public class CheckListRubriqueVM: ObservableRangeCollection<CheckListExigenceVM>, INotifyPropertyChanged
    {
        private ObservableRangeCollection<CheckListExigenceVM> checkListExigence = new ObservableRangeCollection<CheckListExigenceVM>();
        public CheckListRubrique CheckListRubrique { get; set; }
        private bool _expanded;
        public string Name { get { return CheckListRubrique.Name; } }


        public CheckListRubriqueVM(CheckListRubrique checkListRubrique, bool expanded = false)
        {
            this.CheckListRubrique = checkListRubrique;
            this._expanded = expanded;

            foreach (CheckListExigence exigence in CheckListRubrique.Exigences)
            {
                checkListExigence.Add(new CheckListExigenceVM(exigence));
            }
            if (expanded)
                this.AddRange(checkListExigence);

        }

        public CheckListRubriqueVM()
        {
        }

        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                    if (_expanded)
                    {
                        this.AddRange(checkListExigence);
                    }
                    else
                    {
                        this.Clear();
                    }
                }
            }
        }

        public string StateIcon
        {
            get
            {
                if (Expanded)
                {
                    return "arrow_a.png";
                }
                else
                { return "arrow_b.png"; }
            }
        }
    }
}
