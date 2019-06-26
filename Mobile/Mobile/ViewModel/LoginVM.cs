using Mobile.Helpers;
using Mobile.Services;
using Mobile.View;
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
    public class LoginVM: BaseViewModel
    {

        private readonly ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {


                    var current = Connectivity.NetworkAccess;

                    switch (current)
                    {
                        case NetworkAccess.Internet:
                            // Connected to internet
                            var accesstoken = await _apiServices.LoginAsync("Controleur@ocp.ma", "AZERTY123456");

                            Settings.AccessToken = accesstoken;
                            Application.Current.MainPage = new MainPage();
                            break;
                        case NetworkAccess.Local:
                            // Only local network access
                            break;
                        case NetworkAccess.ConstrainedInternet:
                            // Connected, but limited internet access such as behind a network login page
                            break;
                        case NetworkAccess.None:
                            // No internet available
                            IsEmpty = true;
                            CrossToastPopUp.Current.ShowToastMessage("Verifier votre connexion internet et réessayer");
                            break;
                        case NetworkAccess.Unknown:
                            // Internet access is unknown
                            break;
                    }


                    //var accesstoken = await _apiServices.LoginAsync(Username, Password);


                });
            }
        }

        public LoginVM()
        {
            Username = Settings.FullName;
            Password = Settings.Password;
        }

    }
}
