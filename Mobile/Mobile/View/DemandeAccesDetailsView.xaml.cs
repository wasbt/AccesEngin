using Mobile.Model;
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
    public partial class DemandeAccesDetailsView : BaseView
    {
        //DemandeAccesDetailsVM ViewModel;
        public DemandeAccesDetailsView(long Id)
        {
            InitializeComponent();
            var ViewModel = new DemandeAccesDetailsVM(Id);
            BindingContext = ViewModel;
        }
    }
}