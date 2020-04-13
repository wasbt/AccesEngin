using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Mobile.Controls.CustomEntryRenderer), typeof(Mobile.iOS.Renderers.CustomEntryIos))]
namespace Mobile.iOS.Renderers
{
    public class CustomEntryIos : Xamarin.Forms.Platform.iOS.EntryRenderer
    {
        private CoreAnimation.CALayer _line;

        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            _line = null;

            if (Control == null || e.NewElement == null) return;

            Control.BorderStyle = UIKit.UITextBorderStyle.None;

            _line = new CoreAnimation.CALayer
            {
                BorderColor = UIKit.UIColor.FromRGB(174, 174, 174).CGColor,
                BackgroundColor = UIKit.UIColor.FromRGB(174, 174, 174).CGColor,
                Frame = new CoreGraphics.CGRect(0, Frame.Height / 2, Frame.Width * 2, 1f)
            };

            Control.Layer.AddSublayer(_line);
        }
    }
}