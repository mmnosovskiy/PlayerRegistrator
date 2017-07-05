using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlayerRegistrator.Design
{
    [ValueConversion(typeof(string), typeof(int))]
    public class MillisecondsToTimeString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)value);
            return string.Format("{0}:{1}.{2}", ts.Minutes, ts.Seconds, ts.Milliseconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
