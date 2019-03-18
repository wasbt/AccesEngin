using Mobile.Model;
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
	public partial class DemandeAccesDetailsView : ContentPage
	{
        DemandeAccesDetailsVM ViewModel;

        public DemandeAccesDetailsView ()
		{
			InitializeComponent();
		}
        public DemandeAccesDetailsView(DemandeAcces demandeAcces)
        {
            InitializeComponent();

            ViewModel = Resources["vm"] as DemandeAccesDetailsVM;
            ViewModel.DemandeAcces = demandeAcces;
        }
    }
}