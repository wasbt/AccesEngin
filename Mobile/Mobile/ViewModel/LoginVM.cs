using Mobile.Helpers;
using Mobile.Services;
using Mobile.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class LoginVM
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
                        var accesstoken = await _apiServices.LoginAsync(Username, Password);

                        Settings.AccessToken = accesstoken;
                        Application.Current.MainPage = new MainPage();

                    });
                }
            }

            public LoginVM()
            {
                Username = Settings.Username;
                Password = Settings.Password;
            }
        
    }
}
