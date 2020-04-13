using Mobile.Controls;
using Mobile.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Path = System.IO.Path;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabelRenderer), typeof(DroidLabelRenderer))]
namespace Mobile.Droid.Renderers
{

    public class DroidLabelRenderer : LabelRenderer
    {
        public DroidLabelRenderer(Android.Content.Context context) : base(context)
        {

        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;
            var view = (CustomLabelRenderer)Element;

            if (view != null)
            {
                SetIcon(view);
            }
        }
            private void SetIcon(CustomLabelRenderer view)
            {
                if (!string.IsNullOrEmpty(view.Icon))
                {
                    //var resId = Resources.GetIdentifier(view.Icon,"drawable", PackageName)
                    //var resId = (int)typeof(Resource.Drawable).GetField(Path.GetFileNameWithoutExtension(view.Icon)).GetValue(null);

                    try
                    {
                        //Context context => CrossCurrentActivity.Current.Activity;
                        var entry = (CustomLabelRenderer)this.Element;

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
