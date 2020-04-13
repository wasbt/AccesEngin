using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Controls
{
    public class CustomLabelRenderer : Label
    {
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
    }
}
