using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Helpers
{
    public class Settings
    {

        public static string FullName
        {
            get
            {
                return Preferences.Get("fullName", "");
            }
            set
            {
                Preferences.Set("fullName", value);
            }
        }
        public static string UserRoles
        {
            get
            {
                return Preferences.Get("UserRoles", "");
            }
            set
            {
                Preferences.Set("UserRoles", value);
            }
        }

        public static string UserId
        {
            get
            {
                return Preferences.Get("UserId", "");
            }
            set
            {
                Preferences.Set("UserId", value);
            }
        }
        public static string Password
        {
            get
            {
                return Preferences.Get("Password", "");
            }
            set
            {
                Preferences.Set("Password", value);
            }
        }
        public static string AccessToken
        {
            get
            {
                return Preferences.Get("AccessToken", "");
            }
            set
            {
                Preferences.Set("AccessToken", value);
            }
        }
        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return Preferences.Get("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                Preferences.Set("AccessTokenExpirationDate", value);
            }
        }

        public static void ClearSettings()
        {
            Preferences.Clear();
        }

    }
}
