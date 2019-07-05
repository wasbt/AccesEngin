using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
    }
}
