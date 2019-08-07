using Mobile.Helpers;
using Mobile.Services;
using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Mobile
{
    public static class AppHelper
    {
        public static bool IsConnected => Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;
        public static bool IsTokenStillValid => Settings.AccessTokenExpirationDate.Subtract(DateTime.Now).TotalDays > 0;
        public static void SetMainPageAsMasterDetailPage(ContentPage page = null)
        {
            if (page is ContentPage contentPage)
                App.MasterDetailPage.Detail = new NavigationPage(contentPage);

            App.Current.MainPage = App.MasterDetailPage;
            App.MasterDetailPage.IsPresented = false;
        }


        public static async Task<bool> syncControles()
        {
            if (!AppHelper.IsConnected)
            {
                return false;
            }

            Model.TableSql.TableResultatExigenceModel resultat = await getLastControl();
            var resultatApi = new HttpREST.RESTServiceResponse<Model.ResultatExigenceModel>();
            if (resultat == null)
            {
                return false;
            }
            var resultats = new PostResultatExigenceModel();
            var ResultatCheckList = JsonConvert.DeserializeObject<ResultatCheckList>(resultat.ResultatExigencJson);
            resultats.ResultatCheckList = ResultatCheckList;
            resultats.ByteFile = resultat.ItemData;
            resultats.NameFile = resultat.FileName;
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Synchronisation en cours...", configuration: new MaterialLoadingDialogConfiguration { TintColor = Color.FromHex("#289851") }))
            {
                resultatApi = await Api.PostResultatExigencesAsync(resultats);
            }

            if (resultatApi.success)
            {
                await MaterialDialog.Instance.AlertAsync(message: $"Le controle de {resultats.ResultatCheckList.CreatedOn } est traité avec succés", configuration: new MaterialAlertDialogConfiguration { TintColor = Color.FromHex("#289851") });
                var test = await App.Database.DeleteItemAsync(resultat);
                return true;
            }
            else
            {
                await MaterialDialog.Instance.AlertAsync(message: "Échec de synchronisation", configuration: new MaterialAlertDialogConfiguration { TintColor = Color.FromHex("#289851") });
                return false;
            }
        }

        public static async Task<Model.TableSql.TableResultatExigenceModel> getLastControl()
        {
            var listItems = await App.Database.GetItemsAsync();
            var resultat = listItems.LastOrDefault();
            return resultat;
        }
    }
}
