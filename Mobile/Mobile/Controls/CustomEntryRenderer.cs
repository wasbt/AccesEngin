using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Controls
{
    public class CustomEntryRenderer : Entry
    {
        public static readonly BindableProperty EntryColorLineProperty =
          BindableProperty.Create(nameof(EntryLineButtomColor),
              typeof(Color), typeof(CustomEntryRenderer),
              Color.Default);

        public Color EntryLineButtomColor
        {
            get { return (Color)GetValue(EntryColorLineProperty); }
            set { SetValue(EntryColorLineProperty, value); }
        }
        /// <summary> 
        /// The font property 
        /// </summary> 
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(CustomEntryRenderer), string.Empty);

        /// <summary>
        /// Icon file used in Entry
        /// </summary>
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntryRenderer), Color.Default);



        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

    }
}
