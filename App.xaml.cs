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
        const string SETTING_FILE_NAME = "ApplicationData.dat";

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
            Environment.CurrentDirectory = GetWorkDirectory();

            if (File.Exists(SETTING_FILE_NAME))
                IsLight = Convert.ToBoolean(File.ReadAllText(SETTING_FILE_NAME));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            File.WriteAllText(SETTING_FILE_NAME, IsLight + "");
        }

        public string GetWorkDirectory()
        {
            DirectoryInfo info = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NoteKeeper"));

            try
            {
                if (!info.Exists)
                    info.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can`t Save Data", ex.ToString());
            }

            return info.FullName;
        }
    }
}
