using Shared.API.OUT;
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
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
            InitializeComponent();
        }

        public WebViewPage(DemandeDetail demandeDetail)
        {
            InitializeComponent();
            //WebView wv = new WebView();
            //wv.Source = "http://192.168.137.1:3481/Uploades"+demandeDetail.UrlFile;
            //Content = wv;
        }
    }
}