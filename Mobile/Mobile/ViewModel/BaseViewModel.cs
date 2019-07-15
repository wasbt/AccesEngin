﻿using Mobile.Interfaces;
using Mobile.Model.TableSql;
using Mobile.Services;
using Newtonsoft.Json;
using Plugin.Toast;
using PropertyChanged;
using Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Mobile.ViewModel
{
    public class BaseViewModel : BindableObject, INotifyPropertyChanged
    {
        public readonly INavigationService _navigationService;
        public BaseViewModel()
        {
            _navigationService = App.NavigationService;
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        bool isEmpty = false;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                isEmpty = value;
                OnEmptyChanged(this, new PropertyChangedEventArgs("IsEmpty"));
            }
        }

        private void OnEmptyChanged(BaseViewModel baseViewModel, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            CrossToastPopUp.Current.ShowToastMessage("No Data Found");
        }

        string busyText = string.Empty;
        string title = string.Empty;



        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string BusyText
        {
            get => busyText;
            set => SetProperty(ref busyText, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public virtual void OnAppearing()
        {

            Xamarin.Essentials.Connectivity.ConnectivityChanged += async (s, e) =>
            {
                if (e.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "la connexion est rétablie.",
                                                msDuration: MaterialSnackbar.DurationLong,
                                                configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration() { BackgroundColor = Xamarin.Forms.Color.FromHex("#289851") });
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                    {
                        var resultat = App.Database.GetItemsAsync().LastOrDefault();
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
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "Pas de connexion",
                                              msDuration: MaterialSnackbar.DurationLong,
                                              configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration() { BackgroundColor = Xamarin.Forms.Color.FromHex("#DC3545") });

                }
            };
        }

        public virtual void OnDisappearing() { }
        public virtual bool OnBackButtonPressed() { return false; }
    }
}