using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Box.V2.Controls.WP.Common.Converters
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
                return null;

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
                        return null;
                }
            }

            return null;
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
