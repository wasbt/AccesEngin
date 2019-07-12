using Mobile.Extensions;
using Mobile.Model;
using Mobile.Services;
using Mobile.View.PopUp;
using PropertyChanged;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Shared.API.IN;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

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
        public override void OnAppearing()
        {

            base.OnAppearing();
            MessagingCenter.Subscribe<FilterListVM, FilterListDemande>(this, Constants.MESSAGE_FilterList, async (sender, filterModel) =>
            {
                Items.Clear();
                _filterListDemande = filterModel;
                GetListDemende(_filterListDemande);
            });
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshList, async (sender) =>
            {

                Items.Clear();
                GetListDemende(_filterListDemande);

            });
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshControlList, async (callback) =>
            {

                Items.Clear();
                GetListDemende(_filterListDemande);

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
            GetListDemende(_filterListDemande);
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

        private async void GetListDemende(FilterListDemande _filterListDemande)
        {
            var response = (await Api.GetDemandeAccesListAsync(_filterListDemande));

            if (response?.success == true)
            {
                 Items = new ObservableCollection<DemandeAcces>(response.data);
                return;
            }

            Items = new ObservableCollection<DemandeAcces>();
        }

    }
}
