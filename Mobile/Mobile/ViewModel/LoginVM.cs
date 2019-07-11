using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Mobile.View;
using Mobile.View.Menu;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace Mobile.ViewModel
{
    public class LoginVM : BaseViewModel
    {

        private readonly ApiServices _apiServices = new ApiServices();

        public string Username { get; set; } = Settings.FullName = "Controleur@ocp.ma";
        public string Password { get; set; } = Settings.Password = "AZERTY123456";



        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!AppHelper.IsConnected)
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Verifier votre connexion internet et réessayer");
                        return;
                    }
                    var loginModel = new LoginModel();
                    loginModel.Username = Username;
                    loginModel.Password = Password;
                    var login = await Api.LoginAction(loginModel);
                    SetSettings(login);
                    Constants.IsLoggedIn = true;

                    App.MasterDetailPage = new MasterDetailPage
                    {
                        Master = new MenuView(),
                        Detail = new NavigationPage(new ListDemandeView()),
                    };
                    using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Connexion en cours", configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialLoadingDialogConfiguration() { TintColor = Xamarin.Forms.Color.FromHex("#289851") }))
                    {
                        await Task.Delay(5000); // Represents a task that is running.
                    }
                    App.Current.MainPage = App.MasterDetailPage;
                });
            }
        }

        private static void SetSettings(LoginResultModel resultModel)
        {
            Settings.AccessTokenExpirationDate = resultModel.Expires;
            Settings.AccessToken = resultModel.AccessToken;
            Settings.UserId = resultModel.UserId;
            Settings.UserRoles = resultModel.UserRoles;
        }


    }
}
