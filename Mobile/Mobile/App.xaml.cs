using Mobile.Data;
using Mobile.Helpers;
using Mobile.Interfaces;
using Mobile.Services;
using Mobile.View;
using Mobile.View.Menu;
using Newtonsoft.Json;
using Shared.Models;
using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile
{
    public partial class App : Application
    {
        public static INavigationService NavigationService { get; } = new NavigationService();

        public static MasterDetailPage MasterDetailPage;

        private static Database database;

        public static Database Database
        {
            get
            {
                return database ?? (database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "accesEnginsDB.db3")));
            }
        }

        public App()
        {
            InitializeComponent();
            #region Hot Reload
#if DEBUG
            try
            {
                HotReloader.Current.Run(this);
            }
            catch
            {

            }
#endif 
            #endregion
            XF.Material.Forms.Material.Init(this);
            NavigationService.Configure(nameof(Login), typeof(Login));
            NavigationService.Configure(nameof(ListDemandeView), typeof(ListDemandeView));

            MasterDetailPage = new MasterDetailPage
            {
                Master = new MenuView(),
                Detail = new NavigationPage(new ListDemandeView()),
            };


            #region Login Case

            if (AppHelper.IsTokenStillValid)
            {
                if (Constants.IsLoggedIn)
                {
                    AppHelper.SetMainPageAsMasterDetailPage();
                }
                else
                {
                    AppHelper.SetMainPageAsMasterDetailPage(new ListDemandeView());
                    Constants.IsLoggedIn = true;
                }
                Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }


            #endregion


        }

        private async void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            if (AppHelper.IsConnected)
            {
                var res = await AppHelper.syncControles();
                if (res)
                {
                    MessagingCenter.Send(this, Constants.MESSAGE_RefreshList);
                }
            }
        }



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
