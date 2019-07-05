using Mobile.Helpers;
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
                    await _apiServices.LoginAsync(Username, Password);
                    Constants.IsLoggedIn = true;

                    App.MasterDetailPage = new MasterDetailPage
                    {
                        Master = new MenuView(),
                        Detail = new NavigationPage(new ListDemandeView()),
                    };
                    App.Current.MainPage = App.MasterDetailPage;
                });
            }
        }




    }
}
