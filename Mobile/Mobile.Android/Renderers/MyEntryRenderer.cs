using Android.Content.Res;
using Android.Graphics;
using Mobile.Controls;
using Mobile.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntryRenderer), typeof(MyEntryRenderer))]

namespace Mobile.Droid.Renderers
{
    class MyEntryRenderer : EntryRenderer
    {
        private CustomEntryRenderer view;
        public MyEntryRenderer(Android.Content.Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;
            view = (CustomEntryRenderer)Element;

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(view.EntryLineButtomColor.ToAndroid());

            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
        }
    }
}
