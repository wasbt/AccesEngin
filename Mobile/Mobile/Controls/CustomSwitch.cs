using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Controls
{
    public class CustomSwitch : Xamarin.Forms.Switch
    {
        public static readonly BindableProperty SwitchOffColorProperty =
          BindableProperty.Create(nameof(SwitchOffColor),
              typeof(Color), typeof(CustomSwitch),
              Color.Default);

        public Color SwitchOffColor
        {
            get { return (Color)GetValue(SwitchOffColorProperty); }
            set { SetValue(SwitchOffColorProperty, value); }
        }

        public static readonly BindableProperty SwitchOnColorProperty =
          BindableProperty.Create(nameof(SwitchOnColor),
              typeof(Color), typeof(CustomSwitch),
              Color.Default);

        public Color SwitchOnColor
        {
            get { return (Color)GetValue(SwitchOnColorProperty); }
            set { SetValue(SwitchOnColorProperty, value); }
        }

        public static readonly BindableProperty SwitchOnThumbColorProperty =
          BindableProperty.Create(nameof(SwitchOnThumbColor),
              typeof(Color), typeof(CustomSwitch),
              Color.Default);

        public Color SwitchOnThumbColor
        {
            get { return (Color)GetValue(SwitchOnThumbColorProperty); }
            set { SetValue(SwitchOnThumbColorProperty, value); }
        }
           public static readonly BindableProperty SwitchOffThumbColorProperty =
          BindableProperty.Create(nameof(SwitchOffThumbColor),
              typeof(Color), typeof(CustomSwitch),
              Color.Default);

        public Color SwitchOffThumbColor
        {
            get { return (Color)GetValue(SwitchOffThumbColorProperty); }
            set { SetValue(SwitchOffThumbColorProperty, value); }
        }

        public static readonly BindableProperty SwitchThumbImageProperty =
          BindableProperty.Create(nameof(SwitchThumbImage),
              typeof(string),
              typeof(CustomSwitch),
              string.Empty);

        public string SwitchThumbImage
        {
            get { return (string)GetValue(SwitchThumbImageProperty); }
            set { SetValue(SwitchThumbImageProperty, value); }
        }
    }
}