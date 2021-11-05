using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModernBoxes.Tool.convert
{
    public class FileKindConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                    return "#df4853";
                case 1:
                    return "#ffe03b";
                case 2:
                    return "#477bf4";
                case 3:
                    return "#42bf5f";
                default:
                    return "#477bf4";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
