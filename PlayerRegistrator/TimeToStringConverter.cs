using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlayerRegistratorConverter
{
    [ValueConversion(typeof(int), typeof(string))]
    public class TimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)value);
            return ts.ToString(@"hh\:mm\:ss\.ff");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
