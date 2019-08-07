using Mobile.Extensions;
using Mobile.Model;
using Mobile.Services;
using Mobile.View.PopUp;
using Newtonsoft.Json;
using PropertyChanged;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Shared.API.IN;
using Shared.API.OUT;
using Shared.Models;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Mobile.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DemandeAccesVM : BaseViewModel
    {
        public bool isNeedLoadMore = false;
        private bool _isShowLoading = false;
        private const int PageSize = 10;
        private readonly ApiServices _apiServices = new ApiServices();
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

        public bool VisibleIconSync { get; set; } = false;


        public override void OnAppearing()
        {

            base.OnAppearing();

            if (!AppHelper.IsConnected)
            {
                MaterialDialog.Instance.AlertAsync(message: "Verifier votre connexion",
                     configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialAlertDialogConfiguration { TintColor = Color.FromHex("#289851") });
            }

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChangedAsync;


            MessagingCenter.Subscribe<FilterListVM, FilterListDemande>(this, Constants.MESSAGE_FilterList, (sender, filterModel) =>
                {
                    Items.Clear();
                    _filterListDemande = filterModel;
                    GetListDemende(_filterListDemande);
                });
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshList, (sender) =>
            {
                Items.Clear();
                GetListDemende(_filterListDemande);
            });
            MessagingCenter.Subscribe<App>(this, Constants.MESSAGE_RefreshList, (sender) =>
            {
                Items.Clear();
                GetListDemende(_filterListDemande);
                VisibleIconSync = false;
            });
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshControlList, (callback) =>
            {
                Items.Clear();
                GetListDemende(_filterListDemande);
            });

            _filterListDemande = new FilterListDemande();
            Items = new ObservableCollection<ControleModel>();
            GetListDemende(_filterListDemande);
        }

        private async void Connectivity_ConnectivityChangedAsync(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            {
                if (AppHelper.IsConnected)
                {
                    var checkLastItem = await AppHelper.getLastControl();
                    if (checkLastItem != null)
                    {
                        VisibleIconSync = true;
                    }
                    _filterListDemande = new FilterListDemande();
                    Items = new ObservableCollection<ControleModel>();
                    GetListDemende(_filterListDemande);
                }
                else
                {
                    VisibleIconSync = false;
                    await MaterialDialog.Instance.AlertAsync(message: "Verifier votre connexion",
                        configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialAlertDialogConfiguration { TintColor = Color.FromHex("#289851") });
                }
            };
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChangedAsync;
            MessagingCenter.Unsubscribe<FilterListVM>(this, Constants.MESSAGE_FilterList);
            MessagingCenter.Unsubscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshList);
            MessagingCenter.Unsubscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshControlList);
        }

        public DemandeAccesVM()
        {

        }

        public bool IsWorking
        {
            get { return (bool)GetValue(IsWorkingProperty); }
            set { SetValue(IsWorkingProperty, value); }
        }

        public ObservableCollection<ControleModel> Items { get; set; }

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
        public ICommand SyncDataLocalToServer
        {
            get
            {
                return new Command(async () =>
                {
                    var res = await AppHelper.syncControles();
                    if (res)
                    {
                        VisibleIconSync = false;
                        Items.Clear();
                        GetListDemende(_filterListDemande);
                    }
                });
            }
        }

        private async void GetListDemende(FilterListDemande _filterListDemande)
        {
            var response = (await Api.GetDemandeAccesListAsync(_filterListDemande));

            if (response?.success == true)
            {
                Items = new ObservableCollection<ControleModel>(response.data);
                return;
            }

            Items = new ObservableCollection<ControleModel>();
        }

        private static async Task SyncData()
        {

            var listItems = await App.Database.GetItemsAsync();
            var resultat = listItems.LastOrDefault();
            var resultatApi = new HttpREST.RESTServiceResponse<Model.ResultatExigenceModel>();
            if (resultat != null)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Synchronisation en cours..."))
                {


                    var resultats = new PostResultatExigenceModel();
                    var ResultatCheckList = JsonConvert.DeserializeObject<ResultatCheckList>(resultat.ResultatExigencJson);
                    resultats.ResultatCheckList = ResultatCheckList;
                    resultats.ByteFile = resultat.ItemData;
                    resultats.NameFile = resultat.FileName;
                    resultatApi = await Api.PostResultatExigencesAsync(resultats);

                }

                if (resultatApi.success)
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Synchronisation complete");
                    var test = App.Database.DeleteItemAsync(resultat);
                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Échec de synchronisation");
                }

            }
        }
    }

}
