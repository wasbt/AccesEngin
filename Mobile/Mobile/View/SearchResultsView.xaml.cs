using Mobile.ViewModel;
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
	public partial class SearchResultsView : ContentPage
	{
        SearchResultsVM ViewModel;

        public SearchResultsView ()
		{
            InitializeComponent();
            ViewModel = Resources["vm"] as SearchResultsVM;
        }
	}
}