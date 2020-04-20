using Acr.UserDialogs;
using Mobile.Extensions;
using Mobile.Helpers;
using Mobile.Model;
using Mobile.Model.TableSql;
using Mobile.Services;
using Mobile.View;
using Mobile.View.PopUp;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Mobile.ViewModel
{
    public class CheckListRubriqueGroupVM : BaseViewModel
    {
        private MediaFile _mediaFile;

        private readonly ApiServices _apiServices = new ApiServices();

        private CheckListRubriqueVM _oldCheckListRubrique;
        public long DemandeAccesEnginId { get; set; }

        public CheckListExigenceVM InvalidItem { get; set; }

        public CheckListRubriqueGroupVM()
        {

        }
        public bool IsAutorise { get; set; }
        public bool ShowImage { get; set; } = false;
        public byte[] file { get; set; }

        public ICommand GetCheckListByIdCommand
        {
            get
            {
                return new Command<long>(async (key) =>
                {
                    var accessToken = Settings.AccessToken;
                    TypeCheckList = (await Api.GetCheckListByIdAsync(key)).data;
                    Rubriques = TypeCheckList.Rubriques;
                    await ExecuteLoadItemsCommandAsync();
                });
            }
        }

        private ObservableCollection<CheckListRubriqueVM> items;

        public ObservableCollection<CheckListRubriqueVM> Items
        {
            get => items;

            set => SetProperty(ref items, value);
        }



        public Command LoadCheckListRubriqueCommand { get; set; }
        public Command<CheckListRubriqueVM> RefreshItemsCommand { get; set; }

        public CheckListRubriqueGroupVM(long Id)
        {
            DemandeAccesEnginId = Id;
            this.GetCheckListByIdCommand.Execute(Id);
            items = new ObservableCollection<CheckListRubriqueVM>();
            Items = new ObservableCollection<CheckListRubriqueVM>();
            LoadCheckListRubriqueCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());
            RefreshItemsCommand = new Command<CheckListRubriqueVM>((item) => ExecuteRefreshItemsCommand(item));

        }
        public bool isExpanded = true;

        private void ExecuteRefreshItemsCommand(CheckListRubriqueVM item)
        {
            item.Expanded = !item.Expanded;
        }

        private async Task ExecuteLoadItemsCommandAsync()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                Items.Clear();


                if (rubriques != null && rubriques.Count > 0)
                {
                    foreach (var checkListRubrique in TypeCheckList.Rubriques)
                        Items.Add(new CheckListRubriqueVM(checkListRubrique));
                }
                else { IsEmpty = true; }

            }
            catch (Exception ex)
            {
                IsBusy = false;
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private TypeCheckList typeCheckList;

        public TypeCheckList TypeCheckList
        {
            get => typeCheckList;

            set => SetProperty(ref typeCheckList, value);

        }

        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get => imageSource;

            set => SetProperty(ref imageSource, value);
        }

        private List<CheckListRubrique> rubriques;

        public List<CheckListRubrique> Rubriques
        {
            get => rubriques;

            set => SetProperty(ref rubriques, value);
        }

        private ResultatCheckList ResultatCheckList;

        public ICommand AddCommand
        {
            get
            {
                return new Command<ObservableCollection<CheckListRubriqueVM>>(async (Rubriques) =>
                {
                    byte[] imageByte = null;
                    string namefile = null;
                    var resultat = new PostResultatExigenceModel();
                    ResultatCheckList = new ResultatCheckList();
                    ResultatCheckList.ResultatsList = new List<Resultats>();
                    //  var data = Rubrique;
                    ResultatCheckList.DemandeAccesEnginId = DemandeAccesEnginId;
                    ResultatCheckList.CreatedBy = Settings.UserId;
                    ResultatCheckList.CreatedOn = DateTime.Now;
                    ResultatCheckList.IsAutorise = IsAutorise;
                    foreach (var rubrique in Rubriques)
                    {
                        foreach (var exigence in rubrique)
                        {
                            exigence.ColorCellView = "#AFAEAE";
                            InvalidItem = null;
                            var ex = new Resultats();
                            if (exigence.IsHasDate && !exigence.Date.HasValue)
                            {
                                exigence.ColorCellView = "#DC3545";
                                InvalidItem = exigence;
                                return;
                            }
                            else
                            {
                                ex.CheckListExigenceId = exigence.Id;
                                ex.IsConform = exigence.IsConforme;
                                ex.Date = exigence.Date;
                                ex.Observation = exigence.Observation;
                                ResultatCheckList.ResultatsList.Add(ex);

                            }
                        }

                    }
                    var resultDialog = await MaterialDialog.Instance.ConfirmAsync(message: "Merci de confirmer votre action!",
                                       configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialAlertDialogConfiguration { TintColor = Color.FromHex("#2B3673") },
                                       title: "Confirmation",
                                       confirmingText: "Oui",
                                       dismissiveText: "Non");
                    if (resultDialog == true)
                    {
                        resultat.ResultatCheckList = ResultatCheckList;

                        ImageToByte(ref imageByte, ref namefile, resultat);

                        if (AppHelper.IsConnected)
                        {
                            await Api.PostResultatExigencesAsync(resultat);
                        }
                        else
                        {
                            try
                            {
                                var json = Newtonsoft.Json.JsonConvert.SerializeObject(ResultatCheckList);
                                var result = new TableResultatExigenceModel
                                {
                                    ResultatExigencJson = json,
                                    ItemData = imageByte,
                                    FileName = resultat.NameFile
                                };
                                var test = await App.Database.SaveItemAsync(result);
                            }
                            catch (Exception e)
                            {

                            }

                        }
                        await PopupNavigation.Instance.PushPopupSingleAsync(new PopUpSuccessAnimationView());

                        await _navigationService.NavigateAsync(nameof(ListDemandeView));
                    }


                });
            }
        }

        private void ImageToByte(ref byte[] imageByte, ref string namefile, PostResultatExigenceModel resultat)
        {
            if (_mediaFile != null)
            {
                var ImageStream = _mediaFile.GetStream();
                resultat.NameFile = namefile = _mediaFile.Path.Split('\\').LastOrDefault()?.Split('/').LastOrDefault();
                //resultat.StreamFile = ImageStream;
                using (var memoryStream = new MemoryStream())
                {
                    ImageStream.CopyTo(memoryStream);
                    imageByte = memoryStream.ToArray();
                }
                resultat.ByteFile = imageByte;

                _mediaFile.Dispose();
            }
        }

        #region Camera
        public ICommand PickPhotoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                        if (status != PermissionStatus.Granted)
                        {
                            await CrossMedia.Current.Initialize();

                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                UserDialogs.Instance.Alert("No PickPhoto", ":( No PickPhoto available.", "OK");
                                return;
                            }

                            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

                            UserDialogs.Instance.ShowLoading("Chargement...");
                            if (_mediaFile == null)
                            {
                                UserDialogs.Instance.HideLoading();
                                return;
                            }

                            ImageSource = ImageSource.FromStream(() =>
                            {
                                return _mediaFile.GetStream();
                            });
                            ShowImage = true;

                            file = AStreamToByteArray(_mediaFile.GetStream());
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    catch (Exception ex)
                    {

                        //LabelGeolocation.Text = "Error: " + ex;
                    }
                });
            }
        }

        public ICommand TakePhotoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                        if (status != PermissionStatus.Granted)
                        {
                            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                            {
                                UserDialogs.Instance.Alert("Need location", "Gunna need that location", "OK");
                            }

                            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                            if (results.ContainsKey(Permission.Camera))
                            {
                                status = results[Permission.Camera];
                            }
                        }



                        if (status == PermissionStatus.Granted)
                        {

                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                            {
                                UserDialogs.Instance.Alert("No camera", ":( No PickPhoto available.", "OK");
                                return;
                            }
                            UserDialogs.Instance.ShowLoading("Chargement...");

                            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                            {

                            });


                            if (_mediaFile == null)
                            {
                                UserDialogs.Instance.HideLoading();
                                return;
                            }

                            ImageSource = ImageSource.FromStream(() =>
                            {
                                return _mediaFile.GetStream();
                            });
                            ShowImage = true;
                            file = AStreamToByteArray(_mediaFile.GetStream());
                            UserDialogs.Instance.HideLoading();



                        }
                    }
                    catch (Exception ex)
                    {

                        //LabelGeolocation.Text = "Error: " + ex;
                    }
                });
            }
        }

        #endregion

        public static byte[] AStreamToByteArray(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public override bool OnBackButtonPressed()
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(async () =>
            {
                await ConfirmExit();
            });

            return true;
        }
        public async Task ConfirmExit()
        {

            var decision = await MaterialDialog.Instance.ConfirmAsync(message: "êtes vous sur de vouloir fermer cette page?",
                                   configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialAlertDialogConfiguration { TintColor = Color.FromHex("#2B3673") },
                                   title: "Confirmation",
                                   confirmingText: "Oui",
                                   dismissiveText: "Non");

            if (decision == true)
            {
                await _navigationService.NavigateMasterDetailAsync(nameof(ListDemandeView));
            }

        }
    }

}
