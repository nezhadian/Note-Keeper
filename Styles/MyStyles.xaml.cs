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
        Button btnMaximize;

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
            window.StateChanged += Window_StateChanged;

            btnMaximize = (Button)(window.Template as ControlTemplate).FindName("btnMaximize", window);
            ((Button)(window.Template as ControlTemplate).FindName("btnDarkMode", window)).Content = App.IsLight ? "dark_mode" : "light_mode";

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (btnMaximize != null)
                btnMaximize.Content = window.WindowState == WindowState.Normal ? "\ue3c1" : "\ue3e0";
            window.Margin = window.WindowState == WindowState.Normal ? new Thickness(0) : new Thickness(7.3);
        }

        private void ChangeTheme_Clicked(object sender, RoutedEventArgs e)
        {
            App.IsLight = !App.IsLight;
            (sender as Button).Content = App.IsLight ? "dark_mode" : "light_mode";
        }
        #endregion
    }
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
