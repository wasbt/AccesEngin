using Mobile.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModel
{
    public class DemandeAccesDetailsVM : INotifyPropertyChanged
    {
        private DemandeAcces demande;
        public DemandeAcces DemandeAcces
        {
            get { return demande; }
            set
            {
                demande = value;
                OnPropertyChanged("DemandeAcces");
            }
        }

        public DemandeAccesDetailsVM()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}