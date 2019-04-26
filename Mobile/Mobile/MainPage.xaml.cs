using Mobile.MenuItems;
using Mobile.View;
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
            var page1 = new MasterPageItem() { Title = "Demandes non controleés", Icon = "arrow_a.png", TargetType = typeof(ListDemandeView) };
            var page2 = new MasterPageItem() { Title = "Rechercher", Icon = "arrow_a", TargetType = typeof(SearchView) };


            // Adding menu items to menuList
            menuList.Add(page1);
            menuList.Add(page2);
        

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListDemandeView)));
        }

        // Event for Menu Item selection, here we are going to handle navigation based
        // on user selection in menu ListView
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}