using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Note_Keeper
{
    partial class MyStyles : ResourceDictionary
    {
        #region Window

        Window window;

        public MyStyles()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            window.DragMove();
        }

        private void Close_Clicked(object sender, RoutedEventArgs e)
        {
            window.Close();
            if (App.Current.MainWindow == window)
                Environment.Exit(0);
        }

        private void Maxmize_Clicked(object sender, RoutedEventArgs e)
        {
            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Minimize_Button(object sender, RoutedEventArgs e)
        {
            window.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            window = (sender as Window);
        }

        private void ChangeTheme_Clicked(object sender, RoutedEventArgs e)
        {
            App.Settings.IsDarkMode = !App.Settings.IsDarkMode;
        }
        #endregion
    }

    class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return targetType.IsEnum ? Enum.Parse(targetType, parameter.ToString()) : Binding.DoNothing;
        }
    }
}
