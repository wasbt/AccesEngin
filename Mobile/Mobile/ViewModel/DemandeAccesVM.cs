using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class DemandeAccesVM : INotifyPropertyChanged
    {
        private readonly ApiServices _apiServices = new ApiServices();
        private List<DemandeAcces> demandeAcces;

        public DemandeAccesVM()
        {
            this.GetDemandeAccesCommand.Execute(null);
        }
 

        public List<DemandeAcces> DemandeAcces
        {
            get { return demandeAcces; }
            set
            {
                demandeAcces = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetDemandeAccesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    DemandeAcces = await _apiServices.GetDemandeAccesListAsync(accessToken);
                });
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
