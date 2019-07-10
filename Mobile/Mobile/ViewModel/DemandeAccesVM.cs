using Mobile.Extensions;
using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Mobile.View.PopUp;
using PropertyChanged;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Shared.API.IN;
using System.Collections.ObjectModel;
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
        private bool _isShowLoading = false;
        private const int PageSize = 10;
        private readonly ApiServices _apiServices = new ApiServices();
        private ObservableCollection<DemandeAcces> demandeAcces;
        public static readonly BindableProperty IsWorkingProperty =
         BindableProperty.Create(nameof(IsWorking), typeof(bool), typeof(DemandeAccesVM), default(bool));
        //public DemandeAccesVM()
        //{
        //    //this.GetDemandeAccesCommand.Execute(null);
        //}

        public bool IsShowLoading
        {
            get => _isShowLoading;
            set
            {
                _isShowLoading = value;
                OnPropertyChanged();
            }
        }
        private FilterListDemande _filterListDemande;
        public FilterListDemande FilterListDemande
        {
            get => _filterListDemande;
            set
            {
                _filterListDemande = value;
                OnPropertyChanged();
            }
        }
        public override void OnAppearingAsync()
        {

            base.OnAppearingAsync();
            MessagingCenter.Subscribe<FilterListVM, FilterListDemande>(this, Constants.MESSAGE_FilterList, async (sender, filterModel) =>
            {
                Items.Clear();
                _filterListDemande = filterModel;
                StartLoadSomething(_filterListDemande);
            });
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshList, async (sender) =>
            {

                Items.Clear();
                StartLoadSomething(_filterListDemande);

            });
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshControlList, async (callback) =>
            {

                Items.Clear();
                StartLoadSomething(_filterListDemande);

            });


        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshControlList);
        }

        


        public DemandeAccesVM()
        {
              _filterListDemande = new FilterListDemande();
            Items = new ObservableCollection<DemandeAcces>();

            StartLoadSomething(_filterListDemande);


        }


       

        public bool IsWorking
        {
            get { return (bool)GetValue(IsWorkingProperty); }
            set { SetValue(IsWorkingProperty, value); }
        }

        public ObservableCollection<DemandeAcces> Items { get; set; }

        public ICommand RefreshCommand { get; }
        public ICommand OpenPopUpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushPopupSingleAsync(new PopUpFilterListView());
                });
            }
        }

        private async void StartLoadSomething(FilterListDemande _filterListDemande)
        {

            // Load data to property.
            Items = new ObservableCollection<DemandeAcces>(await _apiServices.GetDemandeAccesListAsync(Settings.AccessToken, _filterListDemande));
        }

    }
}
