using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace Note_Keeper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string DATA_FILE_PATH = "ApplicationData.dat";

        private static bool _isLight;
        public static bool IsLight
        {
            get { return _isLight; }
            set {
                _isLight = value;
                ResourceDictionary colorDic = Current.Resources.MergedDictionaries[0];
                colorDic.Source = new Uri(value ? "Colors.xaml" : "Colors_Dark.xaml", UriKind.Relative);
            }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(bool));
                FileStream file = File.OpenRead(DATA_FILE_PATH);
                IsLight = (bool)xml.Deserialize(file);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(bool));
                FileStream file = File.OpenWrite(DATA_FILE_PATH);
                xml.Serialize(file, IsLight);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
