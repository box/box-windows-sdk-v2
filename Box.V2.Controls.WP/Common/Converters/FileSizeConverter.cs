using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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
            return string.Format(culture, "{0:###0.#} {1}", size, sizeText);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
