using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Note_Keeper
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        LoadingPage loadPage;
        EmptyNotebookPage emptyPage;
        HomePage homePage;

        public Home()
        {
            InitializeComponent();
            
            loadPage = new LoadingPage();
            emptyPage = new EmptyNotebookPage();
            homePage = new HomePage();

            DataAccess.OnDeleted += Refresh_Data;
            
        }

        Thread loader = null;
        private void Refresh_Data(object sender, EventArgs e)
        {
            loader?.Abort();
            loader = new Thread(LoadData);
            loader.Start();

        }
        private void LoadData()
        {
            try
            {
                GoToPage(loadPage);

                NoteData[] data = DataAccess.ReadNotesList();

                if (data == null || data.Length == 0)
                {
                    GoToPage(emptyPage);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        homePage.UpdateNoteList(data);
                        container.Content = homePage;
                    },System.Windows.Threading.DispatcherPriority.Loaded);


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GoToPage(Page p)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                container.Content = p;
            }, System.Windows.Threading.DispatcherPriority.Loaded);
        }

        static Home home;
        public static void OpenHome()
        {
            if (home == null)
                home = new Home();

            home.homePage.ClearPreviewData();
            home.Refresh_Data(null, null);
            MainWindow.Navigator.Content = home;
        }
    }
}
