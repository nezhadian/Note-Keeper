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
using System.Windows.Threading;

namespace Note_Keeper
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        DispatcherTimer dateTimeUpdateTimer;
        public HomePage()
        {
            InitializeComponent();
            dateTimeUpdateTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.ApplicationIdle, UpdateNotesDate, Application.Current.Dispatcher);
        }


        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstNotes.SelectedIndex == -1 || ctlNotePreview.Visibility != Visibility.Visible)
                return;

            ctlNotePreview.Data = ((NotePreview)lstNotes.SelectedValue).Data;

        }

        public void UpdateNoteList(NoteData[] data)
        {
            dateTimeUpdateTimer?.Stop();

            lstNotes.Items.Clear();
            
            foreach (var note in data)
            {
                NotePreview newItem = new NotePreview(note);
                lstNotes.Items.Add(newItem);
            }

            dateTimeUpdateTimer?.Start();
        }

        internal void ClearPreviewData()
        {
            ctlNotePreview.ClearData();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 500)
            {
                c1.Width = new GridLength(1, GridUnitType.Star);
                c2.Width = new GridLength(0, GridUnitType.Pixel);
                ctlNotePreview.Visibility = Visibility.Collapsed;
            }
            else
            {
                c1.Width = new GridLength(480, GridUnitType.Pixel);
                c2.Width = new GridLength(1, GridUnitType.Star);
                ctlNotePreview.Visibility = Visibility.Visible;
            }
        }
        
        private void UpdateNotesDate(object sender, EventArgs e)
        {
            foreach (NotePreview note in lstNotes.Items)
            {
                note.RefreshDateTime();
            }
        }
    }
}
