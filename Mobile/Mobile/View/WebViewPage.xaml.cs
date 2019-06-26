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
            WebView wv = new WebView();
            wv.Source = "http://developer.xamarin.com/guides/cross-platform/getting_started/introduction_to_mobile_development/offline.pdf";
            Content = wv;
        }
    }
}