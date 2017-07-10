using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlayerRegistrator
{
    /// <summary>
    /// Converts the <see cref="TimeSpan"/> to the string
    /// </summary>
    public class TimeToStringConverter : BaseValueConverter<TimeToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)value);
            return ts.ToString(@"hh\:mm\:ss\.ff");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
