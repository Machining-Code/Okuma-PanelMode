using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Okuma.PanelMode
{
    public class BoolColorConverter : IValueConverter
    {
        public Color TrueColor { get; set; }
        public Color FalseColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool) && (bool)value)
                return TrueColor;
            else
                return FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
