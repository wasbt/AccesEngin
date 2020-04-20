using FormsToolkit;
using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using Mobile.View;
using Mobile.View.Menu;
using Plugin.Toast;
using Shared;
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

        public string Username { get; set; } = Settings.FullName = "admin";
        public string Password { get; set; } = Settings.Password = "123456";



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
                    var login = await Api.LoginAction(Username, Password);
                    SetSettings(login);
                    if (string.IsNullOrEmpty(login.Error) && !string.IsNullOrEmpty(login.AccessToken))
                    {
                        Constants.IsLoggedIn = true;

                        //App.MasterDetailPage = new MasterDetailPage
                        //{
                        //    Master = new MenuView(),
                        //    Detail = new NavigationPage(new ListDemandeView()),
                        //};
                        using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Connexion en cours", configuration: new XF.Material.Forms.UI.Dialogs.Configurations.MaterialLoadingDialogConfiguration() { TintColor = Xamarin.Forms.Color.FromHex("#2B3673") }))
                        {
                            await Task.Delay(5000); // Represents a task that is running.
                        }

                        var navPage = new NavigationPage(new ListDemandeView());
                        Application.Current.MainPage = navPage;

                        navPage.BarBackgroundColor = Color.FromHex("#202965");
                        navPage.BarTextColor = Color.FromHex("#FFFFFF");

                        //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#202965");
                        //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#FFFFFF");
                        MessagingService.Current.SendMessage(ConstsAccesEngin.ChangeStatutBarColor, (Color)Application.Current.Resources["Primary"]);
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Login ou mot de passe incorrect!");
                        return;
                    }
                    
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
