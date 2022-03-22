using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static NavigationService Navigator { private set; get; }
        public static NavigationService DialogNavigator { private set; get; }
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigator = navigator.NavigationService;
            DialogNavigator = nvgDialog.NavigationService;
            Home.OpenHome();
        }

        private void NewCommand_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            ShowEditor(new EditorPage());
        }
        private void NewCommand_Checked(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public static void ShowEditor(EditorPage editor)
        {
            if (Navigator == null)
                return;

            Navigator.Content = editor;
        }
    }
}
