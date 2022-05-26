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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Note_Keeper
{
    /// <summary>
    /// Interaction logic for NotePreview.xaml
    /// </summary>
    public partial class NotePreview : UserControl
    {
        private const int MAX_LENGTH = 200;
        private NoteData _data;
        public NoteData Data
        {
            get { return _data; }
            set {
                if(value != null)
                {
                    txtTitle.Text = value.Title;
                    txtContent.Text = value.Content.Substring(0, value.Content.Length < MAX_LENGTH ? value.Content.Length : MAX_LENGTH).Replace("\n", " ");
                    _data = value;
                    RefreshDateTime();
                    
                }
            }
        }


        public NotePreview(NoteData data)
        {
            InitializeComponent();
            Data = data; 
        }


        internal void RefreshDateTime()
        {
            txtDate.Text = ConvertToSmoothDate(Data.DateModified);
        }


        public NotePreview() : this(null) { }


        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            new DialogBox(MainWindow.DialogNavigator)
            {
                Message = $"Realy you want to delete \"{Data.Title}\" note",
                ButtonsMode = DialogBox.DialogMode.TwoButton

            }.OnAccept += (s, r) => DataAccess.Delete(Data.Id);
        }


        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowEditor(new EditorPage(Data));
        }
        
        private string ConvertToSmoothDate(DateTime date)
        {
            TimeSpan distance = DateTime.Now - date;

            if (distance.Days == 0)
                if (distance.Hours == 0)
                    if (distance.Minutes == 0)
                        return $"Less than a minute";
                    else
                        return $"{distance.Minutes} Minutes ago";
                else
                    return $"{distance.Hours} Hours ago";

            else if (distance.Days < 7)
                return $"{distance.Days} Days ago";

            else if (distance.Days < 30)
                return $"{distance.Days / 7} Weeks ago";
            else
                return date.ToString();

            //switch (distance.Days)
            //{
            //    case 0:
            //        return "Today";
            //    case 1:
            //        return "YesterDay";
            //    default:
            //        if (distance.Days < 7)
            //            return $"{distance.Days} Days ago";
            //        else if (distance.Days < 30)
            //            return $"{distance.Days / 7} Week ago";
            //        else
            //            return date.ToString();
            //}
        }


        private void OpenSelfContextMenu(object sender, RoutedEventArgs e)
        {
            
            ((Button)sender).ContextMenu.IsOpen = true;
        }
    }
}
