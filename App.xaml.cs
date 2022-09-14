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
        const string SETTING_FILE_NAME = "ApplicationData.xml";
        XmlSerializer xml;

        public static ApplicationSettings Settings { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Environment.CurrentDirectory = GetWorkDirectory();
            xml = new XmlSerializer(typeof(ApplicationSettings));
            try
            {
                if (File.Exists(SETTING_FILE_NAME))
                    Settings = (ApplicationSettings)xml.Deserialize(File.OpenRead(SETTING_FILE_NAME));
                else
                    Settings = new ApplicationSettings();
            }
            catch
            {
                MessageBox.Show("Error In Parsing App Settings XML File");
                Settings = new ApplicationSettings();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            try
            {
                FileStream fs = File.Open(SETTING_FILE_NAME, FileMode.OpenOrCreate);
                xml.Serialize(fs, Settings);
                fs.SetLength(fs.Position);
            }
            catch { }
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
