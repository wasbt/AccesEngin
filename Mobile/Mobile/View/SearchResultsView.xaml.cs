using Mobile.View.Base;
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
	public partial class SearchResultsView : BaseView
    {
        public SearchResultsVM ViewModel;

        public SearchResultsView (long Id)
		{
            InitializeComponent();
           var ViewModel = new SearchResultsVM(Id);
            BindingContext = ViewModel;
        }


        private void BtnBesoinExcep(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!btn.BackgroundColor.Equals(Color.FromHex("#038F7E")))
            {
                btn.BackgroundColor = Color.FromHex("#038F7E");
                btn.TextColor = Color.FromHex("#FFFFFF");
                ListInfoGeneral.IsVisible = true;
                ListResultat.IsVisible = false;
                BtnResultat.BackgroundColor = Color.FromHex("#FFFFFF");
                BtnResultat.TextColor = Color.FromHex("#828491");
            }
        }

        private void BtnBesoinPrev(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!btn.BackgroundColor.Equals(Color.FromHex("#038F7E")))
            {
                btn.BackgroundColor = Color.FromHex("#038F7E");
                btn.TextColor = Color.FromHex("#FFFFFF");
                ListResultat.IsVisible = true;
                ListInfoGeneral.IsVisible = false;
                InfoGeneral.BackgroundColor = Color.FromHex("#FFFFFF");
                InfoGeneral.TextColor = Color.FromHex("#828491");
            }
        }

    }
}