using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ModernBoxes.Tool.convert
{
    public class ContentEmptyBackgroundImageConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "组件应用")
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}