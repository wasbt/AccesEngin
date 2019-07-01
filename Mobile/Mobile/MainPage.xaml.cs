using Mobile.Helpers;
using Mobile.MenuItems;
using Mobile.View;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile
{
    public partial class MainPage : MasterDetailPage
    {

        public List<MasterPageItem> menuList { get; set; }

        public MainPage()
        {

            InitializeComponent();

            menuList = new List<MasterPageItem>();

            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            if (Settings.UserRoles.Contains(ConstsAccesEngin.ROLE_CONTROLEUR))
            {
                var page1 = new MasterPageItem() { Title = "Controler", Icon = "checkList", TargetType = typeof(ListDemandeView) };
                menuList.Add(page1);
            }

            var page2 = new MasterPageItem() { Title = "Rechercher", Icon = "search", TargetType = typeof(SearchView) };
            var page3 = new MasterPageItem() { Title = "Déconnecté", Icon = "logout", TargetType = typeof(Login) };


            // Adding menu items to menuList
            menuList.Add(page2);
            menuList.Add(page3);


            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(SearchView)));
        }

        // Event for Menu Item selection, here we are going to handle navigation based
        // on user selection in menu ListView
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            if (page == typeof(Login))
            {
                App.Current.MainPage = new Login();
                Settings.AccessToken = "";
                Settings.FullName = "";
                Settings.Password = "";
                Settings.UserId = "";
                Settings.UserRoles = "";
            }
            else
            {
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
            }
        }
    }
}