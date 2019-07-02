using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        private static IValueConverter _instance;

        public static IValueConverter Instance
        {
            get => _instance ?? (_instance = new StringToBoolConverter());
            set => _instance = value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = value as string;
            return result?.Length > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception( // you might want to create your own exception type here
                string.Format(
                    "ConvertBack not implemented for value converter type {0}. Either use OneWay binding or update the value converter.",
                    this.GetType()));
        }
    }
}
