using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_PHONE
using System.Windows.Data;
#else
using Windows.UI.Xaml.Data;
using System.Globalization;
#endif

namespace Box.V2.Controls
{
    public class FileSizeConverter : IValueConverter
    {

        public string BytesText { get; set; }
        public string KiloBytesText { get; set; }
        public string MegaBytesText { get; set; }
        public string GigaBytesText { get; set; }

        public float SizeLimit { get; private set; }

        public FileSizeConverter()
        {
            SizeLimit = 921;
        }

#if WINDOWS_PHONE
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#else
        public object Convert(object value, Type targetType, object parameter, string language)
#endif
        {
            if (!(value is long))
            {
                return string.Empty;
            }
            long lsize = (long)value;
            float size = (float)lsize;

            int i = 0;
            while (size >= SizeLimit && ++i <= 3)
            {
                size /= 1024;
            }
            string sizeText;
            switch (i)
            {
                case 0:
                    sizeText = BytesText;
                    break;
                case 1:
                    sizeText = KiloBytesText;
                    break;
                case 2:
                    sizeText = MegaBytesText;
                    break;
                case 3:
                    sizeText = GigaBytesText;
                    break;
                default:
                    sizeText = GigaBytesText;
                    break;
            }

#if WINDOWS_PHONE
            var curCulture = culture;
#else
            var curCulture = CultureInfo.CurrentCulture;
#endif
            return string.Format(curCulture, "{0:###0.#} {1}", size, sizeText);
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
