using Mobile.View.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.View.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpSuccessAnimationView : BasePopUpPage
    {
        public PopUpSuccessAnimationView()
        {
            InitializeComponent();
        }

        private async void Ok_btn_Clicked(object sender, EventArgs e)
        {
            try
            {

                await PopupNavigation.Instance.RemovePageAsync(this);

            }
            catch (Exception ex)
            {

            }
           
        }
    }
}