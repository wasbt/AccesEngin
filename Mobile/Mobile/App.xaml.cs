using Mobile.Interfaces;
using Mobile.Services;
using Mobile.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile
{
    public partial class App : Application
    {
        public static INavigationService NavigationService { get; } = new NavigationService();

        public App()
        {
            InitializeComponent();
           // MainPage = new Login();

#if DEBUG
            HotReloader.Current.Start(this);
#endif
            NavigationService.Configure("Login", typeof(View.Login));

            NavigationService.Configure("ListDemandeView", typeof(View.ListDemandeView));
            //NavigationService.Configure("PushNavigationPage", typeof(View.PushNavigationPage));
            var mainPage = ((NavigationService)NavigationService).SetRootPage("Login");

            MainPage = mainPage;

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
