using System;
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
    /// Interaction logic for NoteLongPreview.xaml
    /// </summary>
    public partial class NoteLongPreview : UserControl
    {
        private NoteData _data;

        public NoteData Data
        {
            get { return _data; }
            set
            {
                if (value != null)
                {
                    txtTitle.Text = value.Title;
                    txtContent.Text = value.Content;
                    btnEdit.Visibility = Visibility.Visible;
                    _data = value;
                }
            }
        }

        public NoteLongPreview(NoteData data)
        {
            InitializeComponent();
        }
        public NoteLongPreview():this(null){}

        


        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowEditor(new EditorPage(Data));
        }

        internal void ClearData()
        {
            txtTitle.Text =
            txtContent.Text = "";
            btnEdit.Visibility = Visibility.Hidden;
        }
    }
}

