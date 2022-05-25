using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace Note_Keeper
{
    class ShadeSolidColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sourceColor = (SolidColorBrush)parameter;

            return new SolidColorBrush(AddToColor(sourceColor.Color, 20));
            

        }

        public static Color AddToColor(Color color,int per)
        {
            color.R = (byte)Math.Max(Math.Min(color.R + per, 255), 0);
            color.G = (byte)Math.Max(Math.Min(color.G + per, 255), 0);
            color.B = (byte)Math.Max(Math.Min(color.B + per, 255), 0);

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class TintSolidColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sourceColor = (SolidColorBrush)parameter;

            return new SolidColorBrush(ShadeSolidColorConverter.AddToColor(sourceColor.Color, -20));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
