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
	public partial class ListDemandeView : ContentPage
	{
		public ListDemandeView ()
		{
			InitializeComponent ();
            ListCC.ItemsSource = new List<Car>() {
                new Car(),
            };

        }
	}
    public class Car
    {

        public int CarID { get; set; }
        public string Make { get; set; }
        public int YearOfModel { get; set; }

    }
}