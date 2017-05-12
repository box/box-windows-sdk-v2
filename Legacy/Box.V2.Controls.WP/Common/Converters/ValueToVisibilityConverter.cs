using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#if WINDOWS_PHONE
using System.Windows.Data;
#else
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
#endif

namespace Box.V2.Controls
{
    public class ValueToVisibilityConverter : IValueConverter
    {
#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value == null || value.ToString().Trim() == string.Empty)
                return Visibility.Collapsed;

            return Visibility.Visible;
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
