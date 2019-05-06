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
    public class DemandeAccesVM : BindableObject
    {
        private bool _isBusy;
        private const int PageSize = 10;
        private readonly ApiServices _apiServices = new ApiServices();
        private InfiniteScrollCollection<DemandeAcces> demandeAcces;
        public static readonly BindableProperty IsWorkingProperty = BindableProperty.Create(nameof(IsWorking), typeof(bool), typeof(DemandeAccesVM), default(bool));

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
            Items = new InfiniteScrollCollection<DemandeAcces>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;
                    if ((Items.Count % PageSize) != 0)
                    {
                        page++;
                    }
                    var accessToken = Settings.AccessToken;

                    var items = await _apiServices.GetDemandeAccesListAsync(accessToken, page, PageSize);

                    if (items.Count() == 0)
                    {
                        IsWorking = false;
                    }
                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                }
            };

            RefreshCommand = new Command(() =>
            {
                // clear and start again
                Items.Clear();
                Items.LoadMoreAsync();
            });

            // load the initial data
            Items.LoadMoreAsync();
        }

        //private async Task DownloadDataAsync()
        //{
        //    var accessToken = Settings.AccessToken;

        //    var items = await _apiServices.GetDemandeAccesListAsync(accessToken, pageIndex: 0, pageSize: PageSize);

        //    DemandeAcces.AddRange(items);
        //}


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool IsWorking
        {
            get { return (bool)GetValue(IsWorkingProperty); }
            set { SetValue(IsWorkingProperty, value); }
        }
        public InfiniteScrollCollection<DemandeAcces> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand { get; }

     
      

    }
}
