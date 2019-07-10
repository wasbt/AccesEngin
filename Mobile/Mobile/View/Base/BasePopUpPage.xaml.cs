using Mobile.ViewModel;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.View.Base
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BasePopUpPage : PopupPage
    {

        public Guid PageInstanceId { get; set; }

        public BasePopUpPage()
        {
            InitializeComponent();
            PageInstanceId = Guid.NewGuid();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var bindingContext = BindingContext as BaseViewModel;

            if (bindingContext != null)
                bindingContext.OnAppearingAsync();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var bindingContext = BindingContext as BaseViewModel;

            if (bindingContext != null)
                bindingContext.OnDisappearing();

        }

    }
}