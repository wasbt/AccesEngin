using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Mobile.View.PopUp;
using PropertyChanged;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Shared.API.IN;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Mobile.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DemandeAccesVM : BaseViewModel
    {
        private static readonly IPopupNavigation _popupNavigation;
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
            MessagingCenter.Subscribe<FilterListVM,FilterListDemande>(this, Constants.MESSAGE_FilterList, async (sender, filterModel) => {
              //  Items.Clear();
               // var items = await _apiServices.GetDemandeAccesListAsync(Settings.AccessToken, filterModel);
               // Items.AddRange(items);
            });
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
                    var filterModel = new FilterListDemande();
                    filterModel.PageIndex = page;
                    filterModel.PageSize = PageSize;
                    var items = await _apiServices.GetDemandeAccesListAsync(accessToken, filterModel);

                    if (items.Count() == 0)
                    {
                        IsWorking = false;
                        IsBusy = false;
                    }
                    IsBusy = false;
                    IsWorking = false;
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

        public bool isWorking;
        public bool IsWorking
        {
            get { return isWorking; }
            set
            {
                isWorking = value;
                OnPropertyChanged();
            }
        }
        public InfiniteScrollCollection<DemandeAcces> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand { get; }
        public ICommand OpenPopUpCommand 
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new PopUpFilterListView());
                });
            }
        }




    }
}
