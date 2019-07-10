using Mobile.Helpers;
using Mobile.Interfaces;
using Mobile.Services;
using Mobile.View;
using Mobile.View.Menu;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile
{
    public partial class App : Application
    {
        public static INavigationService NavigationService { get; } = new NavigationService();

        public static MasterDetailPage MasterDetailPage;

        public static string DatabasePath = string.Empty;

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
