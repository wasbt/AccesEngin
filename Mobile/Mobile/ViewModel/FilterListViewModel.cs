using Mobile.Extensions;
using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Rg.Plugins.Popup.Services;
using Shared.API.IN;
using Shared.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class FilterListVM : BaseViewModel
    {
        private DemandeStatus _demandeStatus; // this is our enum 

        private readonly ApiServices _apiServices = new ApiServices();

        public List<TypeCheckList> TypeCheckList { get; set; }

        public List<DemandeStatus> demandeStatusList;

        public List<DemandeStatus> DemandeStatusList
        {

            get
            {
                return demandeStatusList;
            }
            set
            {
                demandeStatusList = value;
            }
        }

        public Control SelectedControl { get; set; }

        public List<Control> controlList;
        public List<Control> ControlList
        {

            get
            {
                return controlList;
            }
            set
            {
                controlList = value;
            }
        }

        public long _typeCheckListId;

        public DemandeStatus SelectedStatus { get; set; }
        public string Matricule { get; set; }

        public TypeCheckList TypeCheckListId { get; set; }

        public DateTime? _datePlanification = null;

        public DateTime? DatePlanification
        {

            get
            {
                return _datePlanification;
            }
            set
            {
                _datePlanification = value;
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var filterModel = new FilterListDemande();

                    filterModel.StatutId = (int)SelectedStatus;
                    filterModel.TypeCheckListId = TypeCheckListId?.Id;
                    filterModel.DatePlanification = DatePlanification;
                    filterModel.Matricule = Matricule;
                    if (SelectedControl != null)
                        filterModel.OnlyControle = SelectedControl.OnlyControle;

                    MessagingCenter.Send(this, Constants.MESSAGE_FilterList, filterModel);
                    await PopupNavigation.Instance.PopAsync();
                });
            }
        }
        public async override void OnAppearing()
        {
            base.OnAppearing();
            await FillPicker();
        }
        private async Task FillPicker()
        {
            DemandeStatusList = new List<DemandeStatus>()
            {
                DemandeStatus.en_cours_de_traitement,
                DemandeStatus.En_attente,
                DemandeStatus.Refuser,
                DemandeStatus.Autorise,
                DemandeStatus.Non_autorise,
                DemandeStatus.Expirer,
                DemandeStatus.Sortir,
            };

            ControlList = new List<Control>()
            {
                new Control { OnlyControle = false, Name = "Non contrôlée" },
                new Control { OnlyControle = true, Name = "Contrôlée" }
            };

            TypeCheckList = (await Api.GetTypeCheckListAsync()).data;
        }


    }
}
