using Mobile.View.Base;
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
    public partial class Login : BaseView
    {
        public Action DisplayInvalidLoginPrompt;
        //public static LoginView loginView;
        public Login()
        {
            InitializeComponent();
        }

    }
}