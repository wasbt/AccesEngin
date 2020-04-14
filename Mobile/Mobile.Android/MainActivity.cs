using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.Media  ;
using Plugin.CurrentActivity;
using System.IO;
using System.Linq;
using Plugin.FirebasePushNotification;
using Lottie.Forms.Droid;
using FormsToolkit;
using Xamarin.Forms.Platform.Android;
using Shared;

namespace Mobile.Droid
{
    [Activity(Label = "Digi Control", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.FontScale)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            #region Reset BaseTheme & unset splash theme
            base.Window.RequestFeature(WindowFeatures.NoTitle);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);
            #endregion
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


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
            //string dbName = "accesEnginsDB.db3";
            //string dbPath = Path.Combine(folderPath, dbName);
            LoadApplication(new App());
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(255, 255, 255)); //here
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            MessagingService.Current.Subscribe<Xamarin.Forms.Color>(ConstsAccesEngin.ChangeStatutBarColor, (page, color) =>
            {
                try
                {
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                    {
                        // Change the StatutBarColor
                        Window.SetStatusBarColor(new Android.Graphics.Color(color.ToAndroid()));
                        //Window.SetNavigationBarColor(new Android.Graphics.Color(Xamarin.Forms.Color.White.ToAndroid()));
                        //Window.SetDecorCaptionShade(DecorCaptionShade.Light);
                    }
                }
                catch (Exception Ex)
                {
                    Toast.MakeText(this, Ex.Message, ToastLength.Short).Show();
                }
            });
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