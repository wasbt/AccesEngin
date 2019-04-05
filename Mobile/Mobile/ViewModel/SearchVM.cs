using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class SearchVM : BaseViewModel
    {

        private readonly ApiServices _apiServices = new ApiServices();

        private ObservableCollection<DemandeAcces> items;
        public ObservableCollection<DemandeAcces> Items
        {
            get => items;

            set => SetProperty(ref items, value);
        }
        public SearchVM()
        {
            items = new ObservableCollection<DemandeAcces>();
            Items = new ObservableCollection<DemandeAcces>();
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(async (text) =>
                {
                    if (text.Length >= 1)
                    {
                        var accessToken = Settings.AccessToken;
                        Items = await _apiServices.DemandeAccesByMatricule(text, accessToken);
                    }
                }));
            }
        }

        private string _searchText ;
        public string SearchText
        {
            get => _searchText;

            set => SetProperty(ref _searchText, value);
        }
    }
}
