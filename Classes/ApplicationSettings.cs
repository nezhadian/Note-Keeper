using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Note_Keeper
{
    public class ApplicationSettings : DependencyObject
    {


        public bool IsDarkMode
        {
            get { return (bool)GetValue(IsDarkModeProperty); }
            set { 
                ResourceDictionary colorDic = Application.Current.Resources.MergedDictionaries[0];
                colorDic.Source = new Uri(value ? "Colors_Dark.xaml" : "Colors.xaml", UriKind.Relative);
                SetValue(IsDarkModeProperty, value);
            }
        }

        [XmlIgnore]
        public static readonly DependencyProperty IsDarkModeProperty =
            DependencyProperty.Register("IsDarkMode", typeof(bool), typeof(ApplicationSettings), new PropertyMetadata());

    }
}
