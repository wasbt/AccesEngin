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
    }
}
