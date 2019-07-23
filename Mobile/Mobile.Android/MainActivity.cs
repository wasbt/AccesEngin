using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Lottie.Forms.Droid;
using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.Media  ;
using Plugin.CurrentActivity;
using System.IO;
using System.Linq;

namespace Mobile.Droid
{
    [Activity(Label = "Digi Control", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.FontScale)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            string dbName = "accesEnginsDB.db3";

            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
            await CrossMedia.Current.Initialize();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            AnimationViewRenderer.Init();
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //! added using System.IO;
            string dbPath = Path.Combine(folderPath, dbName);
            LoadApplication(new App(dbPath));
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(35, 129, 70)); //here
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // check if the current item id 
            // is equals to the back button id
            if (item.ItemId == 16908332)
            {
                // retrieve the current xamarin forms page instance
                var currentpage = (CoolContentPage)
                Xamarin.Forms.Application.
                Current.MainPage.Navigation.
                NavigationStack.LastOrDefault();

                // check if the page has subscribed to 
                // the custom back button event
                if (currentpage?.CustomBackButtonAction != null)
                {
                    // invoke the Custom back button action
                    currentpage?.CustomBackButtonAction.Invoke();
                    // and disable the default back button action
                    return false;
                }

                // if its not subscribed then go ahead 
                // with the default back button action
                return base.OnOptionsItemSelected(item);
            }
            else
            {
                // since its not the back button 
                //click, pass the event to the base
                return base.OnOptionsItemSelected(item);
            }
        }


        public override void OnBackPressed()
        {

            // retrieve the current xamarin forms page instance
            var currentpage = (CoolContentPage)
            Xamarin.Forms.Application.
            Current.MainPage.Navigation.
            NavigationStack.LastOrDefault();
            if (currentpage?.CustomBackButtonAction != null)
            {
                currentpage?.CustomBackButtonAction.Invoke();
            }
            else
            {
                XF.Material.Droid.Material.HandleBackButton(base.OnBackPressed);
            }
         
        }
    }
}