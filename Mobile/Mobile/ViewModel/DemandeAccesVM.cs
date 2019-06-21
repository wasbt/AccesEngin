using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Mobile.View;
using PropertyChanged;
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
    [AddINotifyPropertyChangedInterface]
    public class DemandeAccesVM : BaseViewModel
    {
        public bool isNeedLoadMore = false;
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
        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshControlList);
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


        public DemandeAccesVM()
        {
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshList, async (sender) => {

                IsWorking = false;
                Items.Clear();
                await Items.LoadMoreAsync();
            });
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshControlList, async (callback) => {

                IsWorking = false;
                Items.Clear();
                await Items.LoadMoreAsync();

            });
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
            //RefreshCommand = new Command(() =>
            //{
            //    // clear and start again
            //    Items.Clear();
            //    Items.LoadMoreAsync();
            //});

            // load the initial data
            Items.LoadMoreAsync();
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
 
        public bool IsWorking { get; set; }
        public InfiniteScrollCollection<DemandeAcces> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand { get; }

     
      

    }
}
