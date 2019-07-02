using Acr.UserDialogs;
using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Mobile.View;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_GoToDetail, async (sender) =>
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                MessagingCenter.Send<DemandeAccesDetailsVM>(this, Constants.MESSAGE_GoToDetail);
                await mdp.Detail.Navigation.PopAsync(true);
            });
        }



        public DemandeDetail DemandeDetail { get; set; }


        public DemandeAccesDetailsVM(long id)
        {
            MessagingCenter.Subscribe<DemandeAccesDetailsVM>(this, Constants.MESSAGE_GoToDetail, async (sender) =>
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                MessagingCenter.Send<DemandeAccesDetailsVM>(this, Constants.MESSAGE_GoToDetail);
                await mdp.Detail.Navigation.PopAsync(true);
            });
            Id = id;
        }

        public override async void OnAppearing()
        {
            MessagingCenter.Subscribe<CheckListRubriqueGroupVM>(this, Constants.MESSAGE_GoToDetail, async (sender) =>
            {
                var mdp = Application.Current.MainPage as MasterDetailPage;
                MessagingCenter.Send(this, Constants.MESSAGE_RefreshControlList);
                await mdp.Detail.Navigation.PopAsync(true);
            });

            DemandeDetailCommand?.Execute(Id);
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
        public ICommand OpenPdfCommand
        {
            get
            {
                return new Command(async () =>
                {

                    try
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                        if (status != PermissionStatus.Granted)
                        {
                            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                            {

                            }

                            var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                            status = results[Permission.Storage];
                        }

                        if (status == PermissionStatus.Granted)
                        {
                            var accessToken = Settings.AccessToken;
                            var Id = DemandeDetail.FileId;
                            //  await _apiServices.DownloadAsync(Id, accessToken);
                            Device.OpenUri(new Uri(Constants.BaseApiAddress + "api/File/" + DemandeDetail.FileId));
                         //   await Xamarin.Essentials.Browser.OpenAsync(Constants.BaseApiAddress+ "api/File/" + DemandeDetail.FileId + ".pdf");

                        }
                        else if (status != PermissionStatus.Unknown)
                        {
                        }
                    }
                    catch (Exception ex)
                    {

                    }



                    //var mdp = Application.Current.MainPage as MasterDetailPage;
                    //await mdp.Detail.Navigation.PushAsync(new WebViewPage(DemandeDetail));
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
                        MessagingCenter.Send<DemandeAccesDetailsVM>(this, Constants.MESSAGE_RefreshList);
                        await mdp.Detail.Navigation.PopAsync(true);
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
                    try
                    {
                        var accessToken = Settings.AccessToken;
                        await _apiServices.ValiderDemandeAsync(result, accessToken);
                        MessagingCenter.Send(this, Constants.MESSAGE_RefreshControlList);
                        var mdp = Application.Current.MainPage as MasterDetailPage;
                        await mdp.Detail.Navigation.PushAsync(new DemandeCheckListAdd(DemandeDetail.Id));
                    }
                    catch (Exception)
                    {

                        throw;
                    }

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
                        try
                        {
                            UserDialogs.Instance.ShowLoading("Chargement...");
                            var accessToken = Settings.AccessToken;
                            DemandeDetail = await _apiServices.GetDetailsDemandeByIdAsync(Id, accessToken);
                            UserDialogs.Instance.HideLoading();
                        }
                        catch (Exception)
                        {
                            UserDialogs.Instance.HideLoading();
                        }

                    }
                }));
            }
        }
    }
}