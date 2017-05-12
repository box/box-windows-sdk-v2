using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_PHONE
using System.Windows.Data;
#else
using Windows.UI.Xaml.Data;
#endif

namespace Box.V2.Controls
{
    public class DateConverter : IValueConverter
    {

#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value == null)
                return null;

            var date = DateTime.Parse(value.ToString(), CultureInfo.CurrentCulture);
            var shortDate = date.ToString("MMM dd, yyyy");
            var shortTime = string.Empty;

            if (parameter != null &&
                !string.IsNullOrWhiteSpace(parameter.ToString()) &&
                parameter.ToString().Equals("NoTimeUnlessToday", StringComparison.OrdinalIgnoreCase))
            {
                if (date.Date == DateTime.Now.Date)
                    shortTime = date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern, CultureInfo.InvariantCulture);
            }
            return string.Format("{0} {1}", shortDate, shortTime); 
        }

#if WINDOWS_PHONE
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#else
        public object ConvertBack(object value, Type targetType, object parameter, string language)
#endif
        {
            throw new NotImplementedException();
        }
    }
}
