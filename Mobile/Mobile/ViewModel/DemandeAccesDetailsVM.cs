using Acr.UserDialogs;
using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Mobile.View;
using PropertyChanged;
using Shared.API.OUT;
using Shared.ENUMS;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DemandeAccesDetailsVM : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();
        long Id = 0;
        public DemandeAccesDetailsVM()
        {

        }



        public DemandeDetail DemandeDetail { get; set; }


        public DemandeAccesDetailsVM(long id)
        {
            Id = id;
        }

        public override async void OnAppearing()
        {
            DemandeDetailCommand?.Execute(Id);
            await Task.Delay(1000);
            base.OnAppearing();

        }


        public ICommand GoToControleCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var mdp = Application.Current.MainPage as MasterDetailPage;
                    await mdp.Detail.Navigation.PushAsync(new DemandeCheckListAdd(DemandeDetail.Id));
                });
            }
        }



        public ICommand RefuserCommand
        {
            get
            {
                return new Command<long>(async (Id) =>
                {
                    PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
                    {
                        InputType = InputType.Name,
                        Placeholder = "Motif",
                        OkText = "Valider",
                        Title = "Valider la demande",
                    });
                    if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
                    {
                        var result = new ValiderDemande()
                        {
                            DemandeAccesEnginId = Id,
                            Motif = pResult.Text,
                            DateSortie = DateTime.Now,
                            StatutDemandeId = (int)DemandeStatus.Refuser

                        };
                        var accessToken = Settings.AccessToken;
                        await _apiServices.ValiderDemandeAsync(result, accessToken);
                        var mdp = Application.Current.MainPage as MasterDetailPage;
                        await mdp.Detail.Navigation.PopAsync();
                        MessagingCenter.Send(this, Settings.MESSAGE_RefreshControlList);
                      //  MessagingCenter.Send<DemandeAccesVM, string>(this, "Alert", "Internet went off.");

                        //var mdp = Application.Current.MainPage as MasterDetailPage;
                        //await mdp.Detail.Navigation.PushAsync(new ListDemandeView());
                    }
                });
            }
        }

        public ICommand ValiderCommand
        {
            get
            {
                return new Command<long>(async (Id) =>
                {
                    var result = new ValiderDemande()
                    {
                        DemandeAccesEnginId = Id,
                        DateSortie = DateTime.Now,
                        StatutDemandeId = (int)DemandeStatus.Accepter
                    };
                    var accessToken = Settings.AccessToken;
                    await _apiServices.ValiderDemandeAsync(result, accessToken);
                    MessagingCenter.Send(this, Settings.MESSAGE_RefreshControlList);
                    var mdp = Application.Current.MainPage as MasterDetailPage;
                    await mdp.Detail.Navigation.PushAsync(new DemandeCheckListAdd(DemandeDetail.Id));
                });
            }
        }

        private ICommand _DemandeDetailCommand;
        public ICommand DemandeDetailCommand
        {
            get
            {
                return _DemandeDetailCommand ?? (_DemandeDetailCommand = new Command<long>(async (Id) =>
                {
                    if (Id > 0)
                    {
                        UserDialogs.Instance.ShowLoading("Chargement...");
                        var accessToken = Settings.AccessToken;
                        DemandeDetail = await _apiServices.GetDetailsDemandeByIdAsync(Id, accessToken);
                        await Task.Delay(2000);
                        UserDialogs.Instance.HideLoading();
                    }
                }));
            }
        }
    }
}