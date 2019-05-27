using Acr.UserDialogs;
using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class CheckListRubriqueGroupVM : BaseViewModel
    {
        private MediaFile _mediaFile;

        private readonly ApiServices _apiServices = new ApiServices();

        private CheckListRubriqueVM _oldCheckListRubrique;
        public long DemandeAccesEnginId { get; set; }


        public CheckListRubriqueGroupVM()
        {

        }

        public ICommand GetCheckListByIdCommand
        {
            get
            {
                return new Command<object>(async (key) =>
                {
                    var accessToken = Settings.AccessToken;
                    TypeCheckList = await _apiServices.GetCheckListByIdAsync(key.ToString(), accessToken);
                    Rubriques = TypeCheckList.Rubriques;
                    await ExecuteLoadItemsCommandAsync();
                });
            }
        }
        public bool IsAutorise { get; set; }

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
            //if (_oldCheckListRubrique == item)
            //{
            //    // click twice on the same item will hide it
            //    item.Expanded = !item.Expanded;
            //}
            //else
            //{
            //if (_oldCheckListRubrique != null)
            //{
            //    // hide previous selected item
            //    _oldCheckListRubrique.Expanded = false;

            //}
            // show selected item
            if (item.Expanded)
            {
                item.Expanded = false;
            }
            else
            {

                item.Expanded = true;
            }
            //}

            //_oldCheckListRubrique = item;
        }

        async Task ExecuteLoadItemsCommandAsync()
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
                            var ex = new Resultats()
                            {
                                CheckListExigenceId = exigence.Id,
                                IsConform = exigence.IsConforme,
                                Date = exigence.Date,
                                Observation = exigence.Observation,
                            };
                            ResultatCheckList.ResultatsList.Add(ex);
                        }

                    }

                    await _apiServices.PostResultatExigencesAsync(ResultatCheckList, Settings.AccessToken);
                });
            }
        }

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

                            if (_mediaFile == null)
                                return;

                            UserDialogs.Instance.Alert("No PickPhoto", _mediaFile.Path, "OK");
                            //FileImage.Source = ImageSource.FromStream(() =>
                            //{
                            //    return _mediaFile.GetStream();
                            //});
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

                            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {
           
                            });


                            if (_mediaFile == null)
                                return;


                            var Source = ImageSource.FromStream(() =>
                            {
                                return _mediaFile.GetStream();
                            });
                        
                            
                        }
                    }
                    catch (Exception ex)
                    {

                        //LabelGeolocation.Text = "Error: " + ex;
                    }
                });
            }
        }

    }

}
