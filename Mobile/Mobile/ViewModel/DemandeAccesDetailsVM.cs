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
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

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



        public ControleModel DemandeDetail { get; set; }


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

        public ICommand GetResultCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var mdp = Application.Current.MainPage as MasterDetailPage;
                    await mdp.Detail.Navigation.PushAsync(new SearchResultsView(DemandeDetail.Id));
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
                            Device.OpenUri(new Uri(AppUrls.BaseUrl + "api/File/" + DemandeDetail.FileId));
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
                    var input = await MaterialDialog.Instance.InputAsync(title: "Valider la demande",
                   confirmingText: "Valider", dismissiveText: "non", inputPlaceholder: "Motif",
                   configuration: new MaterialInputDialogConfiguration { TintColor = Color.FromHex("#289851"),InputTextColor = Color.FromHex("#289851") });

                    //PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
                    //{
                    //    InputType = InputType.Name,
                    //    Placeholder = "Motif",
                    //    OkText = "Valider",
                    //    Title = "Valider la demande",
                    //});

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        var result = new ValiderDemande()
                        {
                            DemandeAccesEnginId = Id,
                            Motif = input,
                            DateSortie = DateTime.Now,
                            StatutDemandeId = (int)DemandeStatus.Refuser

                        };
                        UserDialogs.Instance.ShowLoading("Chargement...");
                        await Api.ValiderDemandeAsync(result);
                        UserDialogs.Instance.HideLoading();
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
                    var decision = await MaterialDialog.Instance.ConfirmAsync(message: "êtes vous sur de vouloir valider cette demande?",
                                 configuration: new MaterialAlertDialogConfiguration { TintColor = Color.FromHex("#289851") },
                                 title: "Confirmation",
                                 confirmingText: "Oui",
                                 dismissiveText: "Non");

                    if (decision == true)
                    {
                        var result = new ValiderDemande()
                        {
                            DemandeAccesEnginId = Id,
                            DateSortie = DateTime.Now,
                            StatutDemandeId = (int)DemandeStatus.En_attente
                        };
                        try
                        {

                            UserDialogs.Instance.ShowLoading("Chargement...");
                            await Api.ValiderDemandeAsync(result);
                            UserDialogs.Instance.HideLoading();
                            MessagingCenter.Send(this, Constants.MESSAGE_RefreshControlList);
                            var mdp = Application.Current.MainPage as MasterDetailPage;
                            await mdp.Detail.Navigation.PushAsync(new DemandeCheckListAdd(DemandeDetail.Id));

                        }
                        catch (Exception)
                        {
                        }
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
                            DemandeDetail = (await Api.GetDetailsDemandeByIdAsync(Id)).data;
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