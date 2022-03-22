using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Note_Keeper
{
    class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] splited = parameter.ToString().Split(' ');

            return value.ToString() == splited[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] splited = parameter.ToString().Split(' ');

            return (bool)value ? Enum.Parse(targetType, splited[0]) : splited.Length == 2 ? Enum.Parse(targetType, splited[1]) : Binding.DoNothing;
            
        }
    }
}
