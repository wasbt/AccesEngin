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
using Xamarin.Forms.Extended;

namespace Mobile.ViewModel
{
    public class DemandeAccesVM : INotifyPropertyChanged
    {
        private bool _isBusy;
        private const int PageSize = 5;
        private readonly ApiServices _apiServices = new ApiServices();
        private InfiniteScrollCollection<DemandeAcces> demandeAcces;


        //public DemandeAccesVM()
        //{
        //    //this.GetDemandeAccesCommand.Execute(null);
        //}

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        public InfiniteScrollCollection<DemandeAcces> DemandeAcces
        {
            get { return demandeAcces; }
            set
            {
                demandeAcces = value;
                OnPropertyChanged();
            }
        }

        //public ICommand GetDemandeAccesCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            var accessToken = Settings.AccessToken;
        //            DemandeAcces = await _apiServices.GetDemandeAccesListAsync(accessToken);
        //        });
        //    }
        //}

        public DemandeAccesVM()
        {
            DemandeAcces = new InfiniteScrollCollection<DemandeAcces>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = DemandeAcces.Count / PageSize;

                    var accessToken = Settings.AccessToken;

                    var items = await _apiServices.GetDemandeAccesListAsync(accessToken, page, PageSize); 

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return DemandeAcces.Count <= 100;
                }
            };

            DownloadDataAsync();
        }

        private async Task DownloadDataAsync()
        {
            var accessToken = Settings.AccessToken;

            var items = await _apiServices.GetDemandeAccesListAsync(accessToken, pageIndex: 0, pageSize: PageSize);

            DemandeAcces.AddRange(items);
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
