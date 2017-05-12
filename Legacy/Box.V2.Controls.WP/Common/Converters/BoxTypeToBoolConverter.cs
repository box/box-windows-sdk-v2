using System;
using System.Collections.Generic;
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
    public class BoxTypeToBoolConverter : IValueConverter
    {
#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (value == null)
                return true;

            BoxItemType type;
            if (Enum.TryParse<BoxItemType>(value.ToString(), out type))
            {
                switch (type)
                {
                    case BoxItemType.File:
                        return false;
                    case BoxItemType.Folder:
                        return true;
                    default:
                        return true;
                }
            }

            return true;
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
