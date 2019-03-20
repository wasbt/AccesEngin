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
	public partial class DemandeCheckListAdd : ContentPage
	{
        TypeCheckListVM ViewModel;

        public DemandeCheckListAdd ()
		{
			InitializeComponent ();
        }

        public DemandeCheckListAdd(string id)
        {
            InitializeComponent();
          BindingContext = new TypeCheckListVM(id);

        }
    }
}