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

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile
{
    public partial class App : Application
    {
        public static INavigationService NavigationService { get; } = new NavigationService();

        public static MasterDetailPage MasterDetailPage;

        public static string DatabasePath = string.Empty;

        static Database database;

        public App(string databasePath)
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
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
           

            #endregion
            DatabasePath = databasePath;

        }

     

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(DatabasePath);
                }
                return database;
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
