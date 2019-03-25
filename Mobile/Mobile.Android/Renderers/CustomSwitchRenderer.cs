﻿using Android.Graphics;
using Android.Widget;
using Mobile.Controls;
using Mobile.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRenderer))]
namespace Mobile.Droid.Renderers
{

    public class CustomSwitchRenderer : SwitchRenderer
    {
        public CustomSwitchRenderer(Android.Content.Context context) : base(context)
        {

        }

        private CustomSwitch view;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;
            view = (CustomSwitch)Element;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                if (this.Control != null)
                {
                    if (this.Control.Checked)
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.SwitchOnColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                        Control.ThumbDrawable.SetColorFilter(view.SwitchOnThumbColor.ToAndroid(), PorterDuff.Mode.Multiply);

                    }
                    else
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                        Control.ThumbDrawable.SetColorFilter(view.SwitchOffThumbColor.ToAndroid(), PorterDuff.Mode.Multiply);

                    }
                    this.Control.CheckedChange += this.OnCheckedChange;
                    UpdateSwitchThumbImage(view);
                }
                //Control.TrackDrawable.SetColorFilter(view.SwitchBGColor.ToAndroid(), PorterDuff.Mode.Multiply);  
            }
        }
        private void UpdateSwitchThumbImage(CustomSwitch view)
        {
            if (!string.IsNullOrEmpty(view.SwitchThumbImage))
            {
                view.SwitchThumbImage = view.SwitchThumbImage.Replace(".jpg", "").Replace(".png", "");
                int imgid = (int)typeof(Resource.Drawable).GetField(view.SwitchThumbImage).GetValue(null);
                //Control.SetThumbResource(Resource.Drawable.icon);
            }
            else
            {
                Control.ThumbDrawable.SetColorFilter(view.SwitchOffThumbColor.ToAndroid(), PorterDuff.Mode.Multiply);
                // Control.SetTrackResource(Resource.Drawable.track);  
            }
        }
        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                this.Control.TrackDrawable.SetColorFilter(view.SwitchOnColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                Control.ThumbDrawable.SetColorFilter(view.SwitchOnThumbColor.ToAndroid(), PorterDuff.Mode.Multiply);

            }
            else
            {
                this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                Control.ThumbDrawable.SetColorFilter(view.SwitchOffThumbColor.ToAndroid(), PorterDuff.Mode.Multiply);

            }
        }
        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }
    }
}