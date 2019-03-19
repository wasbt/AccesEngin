using Mobile.Model;
using Mobile.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

        public ICommand GoToControleCommand
        {
            get
            {
                return new Command<object>(async (key) =>
                {
                     await Application.Current.MainPage.Navigation.PushAsync(new DemandeCheckListAdd(key.ToString()));
                });
            }
        }
      

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}