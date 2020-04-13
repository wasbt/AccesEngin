using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
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
using Color = Xamarin.Forms.Color;
using Path = System.IO.Path;

[assembly: ExportRenderer(typeof(CustomEntryRenderer), typeof(MyEntryRenderer))]

namespace Mobile.Droid.Renderers
{
    class MyEntryRenderer : EntryRenderer
    {
        private CustomEntryRenderer view;
        public MyEntryRenderer(Android.Content.Context context) : base(context)
        {

        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;
            var view = (CustomEntryRenderer)Element;

            if (view != null)
            {
                SetIcon(view);
            }
            GradientDrawable shape = new GradientDrawable();


            shape.SetCornerRadius(5);
            int borderWidth = 2;
            shape.SetStroke(borderWidth, Android.Graphics.Color.Gray);
            Control.SetPadding(30, 40, 30, 40);
            this.Control.SetBackground(shape);

         

            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            //    Control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.White);
            //else
            //    Control.Background.SetColorFilter(Android.Graphics.Color.White, Android.Graphics.PorterDuff.Mode.SrcAtop);
        }


        private void SetIcon(CustomEntryRenderer view)
        {
            if (!string.IsNullOrEmpty(view.Icon))
            {
                //var resId = Resources.GetIdentifier(view.Icon,"drawable", PackageName)
                //var resId = (int)typeof(Resource.Drawable).GetField(Path.GetFileNameWithoutExtension(view.Icon)).GetValue(null);

                try
                {
                    //Context context => CrossCurrentActivity.Current.Activity;
                    var entry = (CustomEntryRenderer)this.Element;

                   // var vv = new Thickness(0, 0, 60, 0);

                    this.Control.CompoundDrawablePadding = 20;
                    var context = Android.App.Application.Context;
                    var resId = context.Resources.GetIdentifier(Path.GetFileNameWithoutExtension(view.Icon), "drawable", context.PackageName);
                    if (resId != 0)
                        Control.SetCompoundDrawablesWithIntrinsicBounds(resId, 0, 0, 0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Control.SetCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);
            }
        }
    }
}
