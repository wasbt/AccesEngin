using FormsToolkit;
using Mobile.View.Base;
using Mobile.ViewModel;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListDemandeView : BaseView
    {
        DemandeAccesVM ViewModel;

        public ListDemandeView ()
		{
			InitializeComponent ();
            // ViewModel = Resources["vm"] as DemandeAccesVM;
            BindingContext = new DemandeAccesVM();


            //Application.Current.Resources = new ResourceDictionary();
            //Application.Current.Resources.Add("UlycesColor", Color.FromRgb(121, 248, 81));
            //var navigationStyle = new Style(typeof(NavigationPage));
            //var barTextColorSetter = new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.Green };
            //var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.Red };

            //navigationStyle.Setters.Add(barTextColorSetter);
            //navigationStyle.Setters.Add(barBackgroundColorSetter);

            //Application.Current.Resources.Add(navigationStyle);


        }
    }
   
}